using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddOrderCommand : ICommand
    {
        public AddOrderCommand(){
            Dishes = new List<int>();
        }

        public Guid OrderId { get; set; }
        public int ChefId { get; set; }
        public int WaiterId { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public List<int> Dishes { get; set; }

    }
}
