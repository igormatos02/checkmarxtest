using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using FluentValidation;
using FluentValidation.Results;
using services.checkmarxs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace application.checkmarx.Commands
{
    public class ChangeOrderStatusCommandHandler : ICommandHandler<ChangeOrderStatusCommand>
    {
        private readonly IApplicationContext _context;
        private readonly IRabbitMQService _rabbitMQService;
        public ChangeOrderStatusCommandHandler(IApplicationContext context, IRabbitMQService rabbitMQService)
        {
            _context = context;
            _rabbitMQService = rabbitMQService;
        }

        public async Task Handle(ChangeOrderStatusCommand command)
        {
            var validator = new ChangeOrderStatusCommandValidator();
            ValidationResult results = validator.Validate(command);
            bool validationSucceeded = results.IsValid;
            if (!validationSucceeded)
            {
                var failures = results.Errors.ToList();
                var message = new StringBuilder();
                failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
                throw new ValidationException(message.ToString());
            }

          

            var order  = _context.Orders.Where(x=>x.OrderId.ToString() == command.OrderId).FirstOrDefault();
            if(order!= null)
            {
                order.Status = command.Status;
                if(order.Status == OrderStatus.Preparing) { 
                    _rabbitMQService.Send(string.Format("Order from table {0} started to be prepared.",order.TableNumber), "deliveryQueue");
                }else if(order.Status == OrderStatus.ReadyToDeliver)
                    _rabbitMQService.Send(string.Format("Order from table {0} id ready to deliver.", order.TableNumber), "deliveryQueue");
            }
           
            await Task.Run(() => { });

        }
    }
}
