using crosscutting.checkmarx.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace domain.entities.checkmarx
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public Guid WaiterId { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreationDate { get; set; }

        public List<int> Dishes { get;set;}
    }
}
