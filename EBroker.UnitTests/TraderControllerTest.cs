using EBroker.Controllers;
using EBroker.Models;
using EBroker.Services.Interfaces;
using EBroker.Utils.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions;

namespace EBroker.UnitTests
{
    /// <summary>
    /// System Under Test
    /// Trade Controller
    /// </summary>
    public class TraderControllerTest
    {
        private readonly Mock<ITradeService> _mockTradeService;
        private readonly Mock<ILogger<TraderController>> _tradeLogger;
        private readonly TraderController traderController;
        private readonly Mock<ITradeHelperWrapper> _mockTradeHelperWrapper;

        public TraderControllerTest()
        {
            _mockTradeService = new Mock<ITradeService>();
            _mockTradeHelperWrapper = new Mock<ITradeHelperWrapper>();
            _tradeLogger = new Mock<ILogger<TraderController>>();
            traderController = new TraderController(_tradeLogger.Object, _mockTradeService.Object,_mockTradeHelperWrapper.Object);
        }

        [Theory]
        [MemberData(nameof(TradeTransactionData))]
        public async Task TraderController_SellEquity_400BadRequest(TraderTransaction traderTransactionRequest, int expectedStatus, string expectedError)
        {
            //Arrange 
            //Act
            var result = await traderController.SellEquityTransaction(traderTransactionRequest) as BadRequestObjectResult;
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }

        [Fact]
        public async Task TraderController_SellEquity_404NotFound_InvalidTrader()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<TraderTransaction>())).ReturnsAsync("Invalid Trader");

            //Act
            var result = await traderController.SellEquityTransaction(traderTransactionRequest) as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Invalid Trader", result.Value);
        }

        [Fact]
        public async Task TraderController_SellEquity_200OK()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<TraderTransaction>()));

            //Act
            var result = await traderController.SellEquityTransaction(traderTransactionRequest) as OkResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task TraderController_SellEquity_404NotFound_InvalidEquity()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.SellEquity(It.IsAny<TraderTransaction>())).ReturnsAsync("Invalid Equity");

            //Act
            var result = await traderController.SellEquityTransaction(traderTransactionRequest) as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Invalid Equity", result.Value);
        }

        [Fact]
        public async Task TraderController_SellEquityTransaction_400BadRequest_OutsideTradingWindow()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(false);
            //Act
            var result = await traderController.SellEquityTransaction(traderTransactionRequest) as BadRequestObjectResult;
            //Assert
            Assert.Equal("Transaction should be placed between 9AM -3PM & Mon -Fri", result.Value);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(TradeTransactionData))]
        public async Task TraderController_BuyEquity_400BadRequest(TraderTransaction traderTransactionRequest, int expectedStatus, string expectedError)
        {
            //Act
            var result = await traderController.BuyEquityTransaction(traderTransactionRequest) as BadRequestObjectResult;
            
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }

        [Fact]
        public async Task TraderController_BuyEquity_404NotFound_InvalidTrader()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() {EquityId =3,TraderId=12,TransactionUnits =12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<TraderTransaction>())).ReturnsAsync("Invalid Trader");

            //Act
            var result = await traderController.BuyEquityTransaction(traderTransactionRequest) as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Invalid Trader", result.Value);
        }

        [Fact]
        public async Task TraderController_BuyEquity_200OK()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<TraderTransaction>()));

            //Act
            var result = await traderController.BuyEquityTransaction(traderTransactionRequest) as OkResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
        }

        [Fact]
        public async Task TraderController_BuyEquity_404NotFound_InvalidEquity()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(true);
            _mockTradeService.Setup(s => s.BuyEquity(It.IsAny<TraderTransaction>())).ReturnsAsync("Invalid Equity");

            //Act
            var result = await traderController.BuyEquityTransaction(traderTransactionRequest) as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Invalid Equity", result.Value);
        }

        [Fact]
        public async Task TraderController_BuyEquity_400BadRequest_OutsideTradingWindow()
        {
            //Arrange
            var traderTransactionRequest = new TraderTransaction() { EquityId = 3, TraderId = 12, TransactionUnits = 12 };
            _mockTradeHelperWrapper.Setup(s => s.IsValidTransactionTime(null)).Returns(false);
            //Act
            var result =await traderController.BuyEquityTransaction(traderTransactionRequest) as BadRequestObjectResult;
            //Assert
            Assert.Equal("Transaction should be placed between 9AM -3PM & Mon -Fri",result.Value);
            Assert.Equal(400,result.StatusCode);
        }

        [Theory]
        [MemberData(nameof(TradeFundData))]
        public async Task TraderController_AddFund_400BadRequest_InValidRequest(TraderFund tradeFundRequest,int expectedStatus,string expectedError)
        {
            //Act
            var result = await traderController.AddFund(tradeFundRequest) as BadRequestObjectResult;
            //Assert
            Assert.Equal(expectedStatus, result.StatusCode);
            Assert.Equal(expectedError, result.Value);
        }

        [Fact]
        public async Task TraderController_AddFund_200Ok()
        {
            //Arrange
            var tradeFundRequest = new TraderFund() { Funds = 5000, Id = 2 };            
            _mockTradeService.Setup(x => x.AddFunds(It.IsAny<TraderFund>())).ReturnsAsync("12500");
            //Act
            var result = await traderController.AddFund(tradeFundRequest) as OkObjectResult;
            //Assert
            Assert.Equal(200, result.StatusCode);
            Assert.Equal("12500", result.Value);
        }

        /// <summary>
        /// 
        /// </summary>
        [Fact]
        public async Task TraderController_AddFund_404NotFound_InValidTrader()
        {
            //Arrange
            var tradeFundRequest = new TraderFund() { Funds = 5000, Id = 2 };
            _mockTradeService.Setup(x => x.AddFunds(It.IsAny<TraderFund>())).ReturnsAsync("Invalid Trader");
            //Act
            var result = await traderController.AddFund(tradeFundRequest) as NotFoundObjectResult;
            //Assert
            Assert.Equal(404, result.StatusCode);
            Assert.Equal("Invalid Trader", result.Value);
        }

        [Fact]
        public void TraderController_AddFund_ThrowsException()
        {
            //Arrange
            var tradeFundRequest = new TraderFund() { Funds = 5000, Id = 2 };
            _mockTradeService.Setup(x => x.AddFunds(It.IsAny<TraderFund>())).Callback(()=>new Exception());
            //Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await traderController.AddFund(tradeFundRequest));

        }

        public static IEnumerable<object[]> TradeTransactionData
        {
            get
            {
                return new List<object[]>
                                {
                new object[] {new TraderTransaction { EquityId = 0 , TraderId = 2, TransactionUnits =3 },400,"EquityId ,TraderId & Transaction Units should be greater than 0" },
                new object[] {new TraderTransaction { EquityId = 4 , TraderId = -5, TransactionUnits =3 },400,"EquityId ,TraderId & Transaction Units should be greater than 0" },
                new object[] {new TraderTransaction { EquityId = 2 , TraderId = 2, TransactionUnits =0 },400, "EquityId ,TraderId & Transaction Units should be greater than 0" }
                };
            }
        }

        public static IEnumerable<object[]> TradeFundData
        {
            get
            {
                return new List<object[]>
                                {
                new object[] {new TraderFund { Id = 0 ,Funds = -300 },400,"TraderId & Funds should be greater than 0" },
                new object[] {new TraderFund { Id = 4 , Funds = 0 },400,"TraderId & Funds should be greater than 0" },
                new object[] {new TraderFund { Id = -4 , Funds = 670 },400, "TraderId & Funds should be greater than 0" }
                };
            }
        }
    }
}
