using domain.entities.checkmarx;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx
{
    public interface IApplicationContext
    {
        public IList<Order> Orders { get; set; }
        public IList<Dish> Dishes { get; set; }
        public IList<Waiter> Waiters { get; set; }
        public IList<Chef> Chefs { get; set; }
    }
}
