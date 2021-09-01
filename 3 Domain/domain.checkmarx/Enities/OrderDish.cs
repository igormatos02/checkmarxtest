using System;
using System.Collections.Generic;
using System.Text;

namespace domain.checkmarx.Enities
{
    public class OrderDish
    {
        public Guid OrderId { get; set; }
        public int DishId { get; set; }
    }
}
