﻿using domain.entities.checkmarx;
using System;
using System.Collections.Generic;
using System.Text;

namespace application.checkmarx.Commands.AddOrder
{
    public class AddOrderCommand : ICommand
    {

        public Guid OrderId { get; set; }
        public Guid WaiterId { get; set; }
        public int TableNumber { get; set; }

        public List<Dish> Dishes { get; set; }

    }
}