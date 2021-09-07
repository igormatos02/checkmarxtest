using application.checkmarx;
using domain.entities.checkmarx;
using Moq;
using System.Collections.Generic;

namespace application.test.checkmarx.MoqBuilders
{
    public static class ContextMoqBuilder
    {
        public static Mock<IApplicationContext> Build()
        {
            Mock<IApplicationContext> applicationContext = new Mock<IApplicationContext>();
            applicationContext.Setup(x => x.Orders).Returns(new List<Order>());
            return applicationContext;
        }
    }
}
