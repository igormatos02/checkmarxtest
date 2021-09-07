using application.checkmarx;
using application.checkmarx.Commands;
using application.checkmarx.Commands.AddOrder;
using application.integrationtest.MoqBuilders;
using crosscutting.checkmarx;
using crosscutting.checkmarx.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using persistence.checkmarx;
using services.checkmarxs;
using System;
using System.Collections.Generic;
using System.Linq;

namespace application.integrationtest.checkmarx
{
    [TestClass]
    public class ChangeOrderStatusCommandRulesTest { 
   
        private readonly IApplicationContext applicationContext;
        private readonly Mock<IRabbitMQService> rabbitMQService;
        private readonly ICommandHandler<ChangeOrderStatusCommand> changeOrderStatusCommandHandler;
        private readonly ICommandHandler<AddOrderCommand> addCommandHandler;
        public ChangeOrderStatusCommandRulesTest()
        {
            applicationContext = new ApplicationContext();
            rabbitMQService = RabitMqMoqBuilder.Build();

            changeOrderStatusCommandHandler = new ChangeOrderStatusCommandHandler(applicationContext, rabbitMQService.Object);
            addCommandHandler = new AddOrderCommandHandler(applicationContext, rabbitMQService.Object);
        }

        [TestMethod]
        public void When_StatusChangesToPrepareButChefIsBusy_Expected_ValidationExeptionForChefIsBusy()
        {
            //Arrange

            // Set The Chef Busy
            var addedOrderGuid = Guid.NewGuid();
            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });

            changeOrderStatusCommandHandler.Handle(new ChangeOrderStatusCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.Preparing,
                ChefId = 1
            });

            // Cofigure a new Task for the Busy Chef
            var newOrderGuid = Guid.NewGuid();
            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = newOrderGuid,
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = newOrderGuid,
                Status = OrderStatus.Preparing,
                ChefId = 1
            };

            //Act
            var result = changeOrderStatusCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.CHEF_IS_BUSY_MSG));
        }

        [TestMethod]
        public void When_StatusChangesToPrepareButChefIsNotBusy_Expected_NoValidationExeptionForChefIsBusy()
        {
            //Arrange

            // Set The Chef Busy
            var addedOrderGuid = Guid.NewGuid();
            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });

            changeOrderStatusCommandHandler.Handle(new ChangeOrderStatusCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.Preparing,
                ChefId = 1
            });

            // Cofigure a new Task for a diferent Chef
            var newOrderGuid = Guid.NewGuid();
            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = newOrderGuid,
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = newOrderGuid,
                Status = OrderStatus.Preparing,
                ChefId = 2
            };

            //Act
            var result = changeOrderStatusCommandHandler.Handle(command);

            //Assert
            Assert.IsFalse(result.IsFaulted);
        }

        [TestMethod]
        public void When_OrderStatusChanges_Expected_OrderRegisterWithStatusChanged()
        {
            //Arrange

            // Set The Chef Busy
            var addedOrderGuid = Guid.NewGuid();
            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });

            changeOrderStatusCommandHandler.Handle(new ChangeOrderStatusCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.Preparing,
                ChefId = 1
            });
           
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = addedOrderGuid,
                Status = OrderStatus.Delivered,
                ChefId = 2
            };

            //Act
            var result = changeOrderStatusCommandHandler.Handle(command);

            var order = applicationContext.Orders.Where(x => x.OrderId == command.OrderId).FirstOrDefault();

            //Assert
            Assert.IsTrue(order.Status == command.Status);
        }
    }
}
