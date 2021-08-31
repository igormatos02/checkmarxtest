using domain.entities.checkmarx;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddOrderCommandHandler : ICommandHandler<AddOrderCommand>
    {
        private readonly IApplicationContext _context;

        public AddOrderCommandHandler(IApplicationContext context)
        {
            _context = context;
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
                Dishes = command.Dishes
            };

            _context.Orders.Add(order);
            //MQueue Send

            //Command must be processed asynchronously, so I used an Emty Task.
            //In the real world, saving data is an asynchronous operation, we use something like _context. SaveChangesAsync ();
            await Task.Run(() => { });

        }
    }
}
