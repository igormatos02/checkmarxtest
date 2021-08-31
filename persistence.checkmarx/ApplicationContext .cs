using application.checkmarx;
using crosscutting.checkmarx.Enums;
using domain.entities.checkmarx;
using System;
using System.Collections.Generic;

namespace persistence.checkmarx
{
    public class ApplicationContext : IApplicationContext
    {
        private List<Order> _orders;
        private List<Dish> _dishes;

        public ApplicationContext()
        {
            _orders = new List<Order>()
            {
                new Order
                {
                    OrderId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e300"),
                    WaiterId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e301"),
                    TableNumber = 1,
                     Status = OrderStatus.SentToKitchen,
                    Dishes = new List<Dish>{}
                },

                new Order
                {
                    OrderId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e302"),
                    WaiterId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e303"),
                    TableNumber = 1,
                    Status = OrderStatus.SentToKitchen,
                    Dishes = new List<Dish>{}
                },
                new Order
                {
                    OrderId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e304"),
                    WaiterId = new Guid("b9eac4cd-016a-4fd2-9d37-40c0a5a9e305"),
                    TableNumber = 1,
                     Status = OrderStatus.SentToKitchen,
                    Dishes = new List<Dish>{}
                },
            };

            _dishes = new List<Dish>()
            {
                new Dish()
                {
                    Id = 1,
                    Description  = "Rice and Meet",
                    PreparationTime = TimeSpan.FromMinutes(30),
                    Price = 40
                },
                new Dish()
                {
                    Id = 1,
                    Description  = "Spagetti",
                    PreparationTime = TimeSpan.FromMinutes(20),
                    Price = 30
                },
                new Dish()
                {
                    Id = 1,
                    Description  = "Soap",
                    PreparationTime = TimeSpan.FromMinutes(10),
                    Price = 15
                }
            };
        }

        public IList<Order> Orders
        {
            get => _orders;
            set => _orders = (List<Order>)value;
        }

        public IList<Dish> Dishes
        {
            get => _dishes;
            set => _dishes = (List<Dish>)value;
        }
    }
}
