using application.checkmarx;
using application.checkmarx.Commands.AddOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using persistence.checkmarx;
using services.checkmarxs;
using System.Threading.Tasks;

namespace application.test.checkmarx
{
    [TestClass]
    public class RabbitMQServiceTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly ICommandHandler<AddOrderCommand> addCommandHandler;
        public RabbitMQServiceTest()
        {
            applicationContext = new ApplicationContext();
            addCommandHandler = new AddOrderCommandHandler(applicationContext, rabbitMQService);

        }

        [TestMethod]
        public void When_MessageIsProduced_ExpectedMessgeInQueue()
        {
           
        }
        [TestMethod]
        public void When_MessageIsConsumed_ExpectedMessgeIsRemovedFromQueue()
        {
           
        }
        [TestMethod]
        public void WhenHostIsNotFound_ExpectHostNotFoundException()
        {
          
        }
        [TestMethod]
        public void When_MessageIsProduced_ExpectedConsumerReceivingMessage()
        {
         
        }
    }
}
