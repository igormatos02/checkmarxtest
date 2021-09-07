using crosscutting.checkmarx;
using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;

namespace application.checkmarx.Commands
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator(Order currentOrder, List<Order> chefOrders)
        {
            
            //Business Rules
            RuleFor(p => IsChefIdSet(p.Status, currentOrder.ChefId)).Equal(false).WithMessage(MessageErrorConstants.CHEF_NOT_SET_MSG);
            RuleFor(p => IsChefBusy(p.Status,chefOrders)).Equal(false).WithMessage(MessageErrorConstants.CHEF_IS_BUSY_MSG);
        }

        private bool IsChefBusy(OrderStatus newStatus, List<Order> chefOrders)
        {
            if(newStatus == OrderStatus.Preparing) {
                var chefIsBusy = chefOrders.Where(x => x.Status == OrderStatus.Preparing).Count();
                return chefIsBusy > 0;
            }

            return false;
        }

        private bool IsChefIdSet(OrderStatus newStatus, int id)
        {
            if (newStatus != OrderStatus.Preparing)
            {
                return id == 0;
            }

            return false;
        }
    }
}
