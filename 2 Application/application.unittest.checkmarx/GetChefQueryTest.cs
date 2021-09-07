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
    public class GetChefQueryTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly IQueryHandler<GetChefsQuery> getChefsQuery;
        public GetChefQueryTest()
        {
            applicationContext = new ApplicationContext();
            getChefsQuery = new GetChefsQueryHandler(applicationContext);

        }

        [TestMethod]
        public void When_GetChefs_ExpectNotEmptyList()
        {

        }

        [TestMethod]
        public void When_GetChefsIsEmpty_ExpectEmptyListException()
        {

        }
    }
}
