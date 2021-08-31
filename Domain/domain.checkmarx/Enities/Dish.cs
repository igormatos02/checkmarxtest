using System;
using System.Collections.Generic;
using System.Text;

namespace domain.entities.checkmarx
{
    public class Dish
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public Decimal Price { get; set; }
    }
}
