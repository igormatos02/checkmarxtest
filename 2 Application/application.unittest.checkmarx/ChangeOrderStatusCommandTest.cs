using application.checkmarx;
using application.checkmarx.Commands;
using application.checkmarx.Commands.AddOrder;
using crosscutting.checkmarx;
using crosscutting.checkmarx.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using persistence.checkmarx;
using services.checkmarxs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace application.test.checkmarx
{
   
   [TestClass]
    public class ChangeOrderStatusCommandTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly ICommandHandler<ChangeOrderStatusCommand> changeOrderStatusCommandHandler;
        public ChangeOrderStatusCommandTest()
        {
            applicationContext = new ApplicationContext();
            changeOrderStatusCommandHandler = new ChangeOrderStatusCommandHandler(applicationContext, rabbitMQService);

        }

        [TestMethod]
        public void When_OrderNotFound_ExpectOrderNotFoundException()
        {

        }
        [TestMethod]
        public void When_OrderStatusChangesToReadyToDeliver_ExpectMessageInDeliveryQueue()
        {

        }
        [TestMethod]
        public void When_OrderStatusChangesToSentToKitch_ExpectMessageInOrderQueue()
        {

        }
        [TestMethod]
        public void When_OrderStatusChangesToPreperaing_Expect_CurrentStatusAsSentToKitchen()
        {
          
        }
        [TestMethod]
        public void When_OrderStatusChangesToReadyToDeliver_Expect_CurrentStatusAsPreparing()
        {
            
        }
        [TestMethod]
        public void When_OrderStatusChangesDelivered_Expect_CurrentStatusAsReadyToDeliver()
        {
           
        }

        [TestMethod]
        public void When_ChefIdNotSet_Expected_ValidationExeptionForChefMustBeSelected()
        {
            //Arrange
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                ChefId = 1

            };

            //Act
            var result = changeOrderStatusCommandHandler.Handle(command);

            //Assert
            Assert.IsTrue(result.IsFaulted);
            Assert.IsTrue(result.Exception.InnerException.ToString().Contains(MessageErrorConstants.CHEF_NOT_SET_MSG));
        }

        [TestMethod]
        public void When_ChefIdIsSet_Expected_NoValidationExeptionForChefId()
        {
            //Arrange
            var command = new ChangeOrderStatusCommand()
            {
                OrderId = Guid.NewGuid(),
                Status = OrderStatus.SentToKitchen,
                ChefId = 1

            };

            //Act
            var result = changeOrderStatusCommandHandler.Handle(command);

            //Assert
            Assert.IsFalse(result.IsFaulted);
        }


    }
}
