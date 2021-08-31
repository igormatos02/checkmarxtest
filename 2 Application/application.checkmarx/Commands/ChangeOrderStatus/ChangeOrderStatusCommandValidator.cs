using crosscutting.checkmarx.Enums;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Commands
{
    public class ChangeOrderStatusCommandValidator : AbstractValidator<ChangeOrderStatusCommand>
    {
        public ChangeOrderStatusCommandValidator()
        {
           // RuleFor(p => p.Status).Equals(OrderStatus.Preparing).WithMessage("Order cannot be empty");
        }
    }
}
