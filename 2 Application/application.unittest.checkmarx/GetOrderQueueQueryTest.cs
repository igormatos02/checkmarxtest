using application.checkmarx;
using application.checkmarx.Commands;
using application.checkmarx.Commands.AddOrder;
using application.checkmarx.Queries;
using crosscutting.checkmarx.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using persistence.checkmarx;
using services.checkmarxs;
using System.Threading.Tasks;

namespace application.test.checkmarx
{
    public class GetOrderQueueQueryTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly IQueryHandler<GetOrderQueueQuery> getOrderQueueQuery;
        public GetOrderQueueQueryTest()
        {
            applicationContext = new ApplicationContext();
            getOrderQueueQuery = new GetOrderQueueQueryHandler(applicationContext);

        }

        [TestMethod]
        public void When_GetQueueWithStatusNone_ExpectResultWithAllStatus()
        {

        }
        [TestMethod]
        public void When_GetQueueWithSpecificStatus_ExpectResultWithTheSpecificStatus(OrderStatus status)
        {

        }
        [TestMethod]
        public void When_OrderStatusChangesDelivered_Expect_CurrentStatusAsReadyToDeliver()
        {

        }
    }
}
