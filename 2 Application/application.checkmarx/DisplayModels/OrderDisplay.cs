using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.DisplayModels
{
    public class OrderDisplay : IResult
    {
        public Guid OrderId { get; set; }
        public int WaiterId { get; set; }
        public int ChefId { get; set; }
        public int TableNumber { get; set; }
        public OrderStatus Status { get; set; }

        public DateTime CreationDate { get; set; }

        public List<int> Dishes { get; set; }
    }
}
