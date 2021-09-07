using crosscutting.checkmarx;
using Moq;
using services.checkmarxs;

namespace application.test.checkmarx.MoqBuilders
{

    public static class RabitMqMoqBuilder
    {
        public static Mock<IRabbitMQService> Build()
        {
            Mock<IRabbitMQService> rabitMqService = new Mock<IRabbitMQService>();
            rabitMqService.Setup(x => x.Send("Test Message", AppConstants.ORDER_QUEUE));
            return rabitMqService;
        }
    }
}
