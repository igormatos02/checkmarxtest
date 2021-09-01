using application.checkmarx;
using application.checkmarx.Commands;
using application.checkmarx.Commands.AddOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using persistence.checkmarx;
using services.checkmarxs;
using System.Threading.Tasks;

namespace application.test.checkmarx
{
   
   [TestClass]
    public class ChangeOrderStatusCommandTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly ICommandHandler<ChangeOrderStatusCommand> changeOrderStatusCommand;
        public ChangeOrderStatusCommandTest()
        {
            applicationContext = new ApplicationContext();
            changeOrderStatusCommand = new ChangeOrderStatusCommandHandler(applicationContext, rabbitMQService);

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
       
       
    }
}
