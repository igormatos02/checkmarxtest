using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using FluentValidation;
using FluentValidation.Results;
using services.checkmarxs;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        private readonly IApplicationContext _context;
        private readonly IRabbitMQService _rabbitMQService;

        public AddOrderCommandHandler(IApplicationContext context, IRabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }

        public async Task Handle(AddOrderCommand command)
        {
            var validator = new AddOrderCommandValidator();
            ValidationResult results = validator.Validate(command);
            bool validationSucceeded = results.IsValid;
            if (!validationSucceeded)
            {
                var failures = results.Errors.ToList();
                var message = new StringBuilder();
                failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
                throw new ValidationException(message.ToString());
            }

            var order = new Order
            {
                OrderId = command.OrderId,
                WaiterId = command.WaiterId,
                TableNumber = command.TableNumber,
                Dishes = command.Dishes,
                CreationDate = DateTime.Now,
                Status = OrderStatus.SentToKitchen

            };

            _context.Orders.Add(order);
            _rabbitMQService.Send("New Order", "OrderQueue");

            await Task.Run(() => { });

        }
    }
}
