using crosscutting.checkmarx;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddOrderCommandValidator : AbstractValidator<AddOrderCommand>
    {
        public AddOrderCommandValidator()
        {
           
            RuleFor(p => p.WaiterId).GreaterThan(0).WithMessage(MessageErrorConstants.WAITER_NOT_SET_MSG);
            RuleFor(p => p.ChefId).GreaterThan(0).WithMessage(MessageErrorConstants.CHEF_NOT_SET_MSG);
            RuleFor(p => p.TableNumber).GreaterThan(0).WithMessage(MessageErrorConstants.TABLE_NOT_SET_MSG);
            RuleFor(p => p.Dishes).NotEmpty().WithMessage(MessageErrorConstants.EMPTY_DISHES_MSG);
           
        }
    }
}
