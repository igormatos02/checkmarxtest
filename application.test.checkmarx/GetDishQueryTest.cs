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
    public class GetDishQueryTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly IQueryHandler<GetDishesQuery> getDishesQuery;
        public GetDishQueryTest()
        {
            applicationContext = new ApplicationContext();
            getDishesQuery = new GetDishesQueryHandler(applicationContext);

        }

        [TestMethod]
        public void When_GetDishes_ExpectNotEmptyList()
        {

        }

        [TestMethod]
        public void When_GetDishesIsEmpty_ExpectEmptyListException()
        {

        }
    }
}
