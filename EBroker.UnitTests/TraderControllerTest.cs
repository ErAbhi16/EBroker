using EBroker.Controllers;
using EBroker.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace EBroker.UnitTests
{
    public class TraderControllerTest
    {
        private readonly Mock<ITradeService> tradeService;
        private readonly Mock<ILogger<TraderController>> tradeLogger;
        private readonly TraderController traderController;

        public TraderControllerTest()
        {
            tradeService = new Mock<ITradeService>();
            tradeLogger = new Mock<ILogger<TraderController>>();
            traderController = new TraderController(tradeLogger.Object, tradeService.Object);
        }

        [Fact]
        public void TraderController_ShoulReturn_200Ok_ValidEquity()
        {

        }
    }
}
