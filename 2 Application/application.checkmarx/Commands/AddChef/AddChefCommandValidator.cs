using crosscutting.checkmarx;
using domain.entities.checkmarx;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddChefCommandValidator : AbstractValidator<AddChefCommand>
    {
        public AddChefCommandValidator(List<Chef> chefs)
        {
           
            RuleFor(p => p.ChefId).GreaterThan(0).WithMessage(MessageErrorConstants.CHEF_EMPTY_ID_MSG);
            RuleFor(p => p.Name).NotEqual(string.Empty).WithMessage(MessageErrorConstants.CHEF_EMPTY_NAME_MSG);
            
            //Business Rules
            RuleFor(p => DoesChefIdExist(p.ChefId, chefs)).Equal(false).WithMessage(MessageErrorConstants.CHEF_DUPLICATE_ID_MSG);

        }

        private bool DoesChefIdExist(int newId, List<Chef> chefs)
        {
            return chefs.Select(x => x.Id).Contains(newId);
        }
    }
}
