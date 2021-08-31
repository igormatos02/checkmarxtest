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
           
            RuleFor(p => p.WaiterId).GreaterThan(0).WithMessage("Waiter must be selected");
            RuleFor(p => p.TableNumber).GreaterThan(0).WithMessage("Table must be selected");
            RuleFor(p => p.Dishes).NotEmpty().WithMessage("Dishes mst be added to the order");
        }
    }
}
