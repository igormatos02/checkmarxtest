using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Commands
{
    public class ChangeOrderStatusCommand : ICommand
    {
        public Guid OrderId { get; set; }
        public int ChefId { get; set; }
        public OrderStatus Status { get; set; }
    }
}
