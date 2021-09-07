using application.checkmarx;
using application.checkmarx.Commands.AddOrder;
using application.test.checkmarx.MoqBuilders;
using crosscutting.checkmarx;
using crosscutting.checkmarx.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using services.checkmarxs;
using System;
using System.Collections.Generic;

namespace application.test.checkmarx
{
    [TestClass]
    public class AddOrderCommandTest
    {
        private readonly Mock<IApplicationContext> applicationContext;
        private readonly Mock<IRabbitMQService> rabbitMQService;
        private readonly ICommandHandler<AddOrderCommand> addCommandHandler;
        public AddOrderCommandTest()
        {
            applicationContext = ContextMoqBuilder.Build();
            rabbitMQService = RabitMqMoqBuilder.Build();

            addCommandHandler = new AddOrderCommandHandler(applicationContext.Object, rabbitMQService.Object);
        }

        [TestMethod]
        public void When_WaiterIdNotSet_Expected_ValidationExeptionForWaiterMustBeSelected()
        {
            //Arrange
            var command = new AddOrderCommand() { 
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                ChefId = 1,
                Dishes = new List<int> {1,2,3}
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.WAITER_NOT_SET_MSG));
        }

        [TestMethod]
        public void When_WaiterIdIsSet_Expected_NoValidationExeptionForWaiterId()
        {
            //Arrange
            var command = new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                ChefId = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsFalse(result.IsFaulted);
        }

        [TestMethod]
        public void When_TableNumberIdNotSetOrIsLessThanZero_Expect_ValidationExeptionForTableNumber()
        {
            //Arrange
            var command = new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = -1,
                ChefId = 1,
                Dishes = new List<int> { 1, 2, 3 }
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.TABLE_NOT_SET_MSG));
        }

        [TestMethod]
        public void When_TableNumberIdIsSet_Expect_NoValidationExeptionForTableNumber()
        {
            //Arrange
            var command = new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                ChefId = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsFalse(result.IsFaulted);
        }

        [TestMethod]
        public void When_CreationDateIsNotaValidDate_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
      
        [TestMethod]
        public void When_OrderHasNopDishes_Expect_ValidationExeptionForEmptyDishes()
        {
            var command = new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = -1,
                WaiterId = 1,
                ChefId = 1
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.EMPTY_DISHES_MSG));
        }

        [TestMethod]
        public void When_OrderHasDishes_Expect_NoValidationExeptionForEmptyDishes()
        {
            var command = new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                WaiterId = 1,
                ChefId = 1,
                Dishes = new List<int> { 1, 2, 3 },
            };

            //Act
            var result = addCommandHandler.Handle(command);

            //Assert
            Assert.IsFalse(result.IsFaulted);
           
        }
        [TestMethod]
        public void When_OrderIsCreated_ExpectMessageInOrderQueue()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
    }
}
