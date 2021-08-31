using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.DisplayModels
{
    public class DishDisplay : IResult
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public TimeSpan PreparationTime { get; set; }
        public Decimal Price { get; set; }
    }
}
