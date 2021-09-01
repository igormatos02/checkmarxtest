using application.checkmarx;
using application.checkmarx.Commands.AddOrder;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using persistence.checkmarx;
using services.checkmarxs;
using System.Threading.Tasks;

namespace application.test.checkmarx
{
    [TestClass]
    public class AddOrderCommandTest
    {
        private readonly IApplicationContext applicationContext;
        private readonly IRabbitMQService rabbitMQService;
        private readonly ICommandHandler<AddOrderCommand> addCommandHandler;
        public AddOrderCommandTest()
        {
            applicationContext = new ApplicationContext();
            addCommandHandler = new AddOrderCommandHandler(applicationContext, rabbitMQService);

        }

        [TestMethod]
        public  void When_WaiterIdNotSet_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() {});
        }
        [TestMethod]
        public void When_WaiterIdNotInteger_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_ChefIdNotInteger_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_OrderIsNotaValidGuid_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_TableNumberNotInteger_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_CreationDateIsNotaValidDate_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_TableNumberIdNotSet_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_OrderHasNopDishes_Expect_ExpectedValidationExeption()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
        [TestMethod]
        public void When_OrderIsCreated_ExpectMessageInOrderQueue()
        {
            var result = addCommandHandler.Handle(new AddOrderCommand() { });
        }
    }
}
