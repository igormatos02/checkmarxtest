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
            RuleFor(p => p.Dishes).NotEmpty().WithMessage("Order cannot be empty");
        }
    }
}
