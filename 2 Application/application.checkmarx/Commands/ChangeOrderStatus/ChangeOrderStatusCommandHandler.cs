using domain.entities.checkmarx;
using FluentValidation;
using FluentValidation.Results;
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

        public ChangeOrderStatusCommandHandler(IApplicationContext context)
        {
            _context = context;
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

            }
            //MQueue Send

            //Command must be processed asynchronously, so I used an Emty Task.
            //In the real world, saving data is an asynchronous operation, we use something like _context. SaveChangesAsync ();
            await Task.Run(() => { });

        }
    }
}
