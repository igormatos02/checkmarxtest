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
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.Preparing,
                ChefId = 1
            };

            addCommandHandler.Handle(new AddOrderCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                CreationDate = DateTime.Now,
                TableNumber = 1,
                ChefId = 1,
                Dishes = new List<int> { 1, 2, 3 },
                WaiterId = 1
            });

            //Act


            var result = changeOrderStatusCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.CHEF_IS_BUSY_MSG));
        }
    }
}
