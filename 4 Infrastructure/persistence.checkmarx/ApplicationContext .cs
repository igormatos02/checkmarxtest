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
        private List<Waiter> _waiters;
        private List<Chef> _chefs;

        public ApplicationContext()
        {
            _orders = new List<Order>()
            {
              
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
                    Id = 2,
                    Description  = "Spagetti",
                    PreparationTime = TimeSpan.FromMinutes(20),
                    Price = 30
                },
                new Dish()
                {
                    Id = 3,
                    Description  = "Soap",
                    PreparationTime = TimeSpan.FromMinutes(10),
                    Price = 15
                }
                ,
                new Dish()
                {
                    Id = 4,
                    Description  = "Fish and Potatos",
                    PreparationTime = TimeSpan.FromMinutes(40),
                    Price = 35
                }
            };

            _chefs = new List<Chef>
            {
                new Chef{ Id=1, Name="Ronaldo"}
            };

            _waiters = new List<Waiter>
            {
                 new Waiter{ Id=1, Name="Jhon"},
                 new Waiter{ Id=2, Name="Paul"}
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

        public IList<Waiter> Waiters
        {
            get => _waiters;
            set => _waiters = (List<Waiter>)value;
        }

        public IList<Chef> Chefs
        {
            get => _chefs;
            set => _chefs = (List<Chef>)value;
        }
    }
}
