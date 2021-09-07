using crosscutting.checkmarx;
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
    public class AddChefCommandHandler : ICommandHandler<AddChefCommand>
    {
        private readonly IApplicationContext _context;

        public AddChefCommandHandler(IApplicationContext context)
        {
            _context = context;
        }

        public async Task Handle(AddChefCommand command)
        {
            var chefs = _context.Chefs.ToList();
            var validator = new AddChefCommandValidator(chefs);
            ValidationResult results = validator.Validate(command);
            bool validationSucceeded = results.IsValid;
            if (!validationSucceeded)
            {
                var failures = results.Errors.ToList();
                var message = new StringBuilder();
                failures.ForEach(f => { message.Append(f.ErrorMessage + Environment.NewLine); });
                throw new ValidationException(message.ToString());
            }

            var chef = new Chef
            {
                Id = command.ChefId,
                Name = command.Name
            };

            _context.Chefs.Add(chef);
           
            await Task.Run(() => { });

        }
    }
}
