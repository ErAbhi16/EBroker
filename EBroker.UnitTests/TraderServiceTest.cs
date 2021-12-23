using EBroker.Data.Repositories;
using EBroker.Data.Repositories.Interfaces;
using EBroker.Models;
using EBroker.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace EBroker.UnitTests
{
    /// <summary>
    /// System Under Test
    /// Trade Service
    /// </summary>
    public class TradeServiceTest
    {
        private readonly Mock<ITradeRepository> _mockTradeRepository;

        private readonly TradeService _tradeService;

        public TradeServiceTest()
        {
            _mockTradeRepository = new Mock<ITradeRepository>();
            _tradeService = new TradeService(_mockTradeRepository.Object);
        }


        [Fact]
        public async Task TradeService_BuyEquity_InvalidEquity()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction();
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync((Data.Entities.Equity)null);

            //Act
            var result = await _tradeService.BuyEquity(traderTransaction);

            //Assert
            Assert.Equal("Invalid Equity", result);
        }

        [Fact]
        public async Task TradeService_BuyEquity_InvalidTrader()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction();

            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Data.Entities.Trader)null);

            //Act
            var result = await _tradeService.BuyEquity(traderTransaction);

            //Assert
            Assert.Equal("Invalid Trader", result);
        }


        [Fact]
        public async Task TradeService_BuyEquity_InsufficientFunds()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId =2 , TraderId =1, TransactionUnits=3};
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2 ,UnitPrice =200};
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1 , Funds =400};

            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);

            //Act
            var result = await _tradeService.BuyEquity(traderTransaction);

            //Assert
            Assert.Equal("InSufficient Funds", result);
        }

        [Fact]
        public async Task TradeService_BuyEquity_Success_AddTraderHoldings()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 2, TraderId = 1, TransactionUnits = 3 };
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2, UnitPrice = 200 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };

            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.AddTraderTransaction(It.IsAny<Data.Entities.TraderTransaction>()));
            _mockTradeRepository.Setup(x => x.AddTraderHoldings(It.IsAny<Data.Entities.TraderHolding>()));
            _mockTradeRepository.Setup(x => x.GetTraderHoldingsByEquityTraderId(It.IsAny<int>(),It.IsAny<int>()));
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.BuyEquity(traderTransaction);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_BuyEquity_SuccessfullyBought_UpdateTraderHoldings()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 2, TraderId = 1, TransactionUnits = 3 };
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2, UnitPrice = 200 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };
            var traderHoldings = new Data.Entities.TraderHolding() { EquityId = 2, TraderId = 1, UnitHoldings = 10 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.AddTraderTransaction(It.IsAny<Data.Entities.TraderTransaction>()));
            _mockTradeRepository.Setup(x => x.UpdateTraderHoldings(It.IsAny<Data.Entities.TraderHolding>()));
            _mockTradeRepository.Setup(x => x.GetTraderHoldingsByEquityTraderId(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(traderHoldings);
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.BuyEquity(traderTransaction);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_SellEquity_InvalidEquity()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction();
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync((Data.Entities.Equity)null);

            //Act
            var result = await _tradeService.SellEquity(traderTransaction);

            //Assert
            Assert.Equal("Invalid Equity", result);
        }

        [Fact]
        public async Task TradeService_SellEquity_InvalidTrader()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction();

            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Data.Entities.Trader)null);

            //Act
            var result = await _tradeService.SellEquity(traderTransaction);

            //Assert
            Assert.Equal("Invalid Trader", result);
        }

        [Fact]
        public async Task TradeService_SellEquity_InsufficientSellingUnits()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 2, TraderId = 1, TransactionUnits = 1 };
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2, UnitPrice = 100 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };
            var traderHoldings = new Data.Entities.TraderHolding() { EquityId = 2, TraderId = 1, UnitHoldings = 10 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.GetTraderHoldingsByEquityTraderId(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync((Data.Entities.TraderHolding)null);
                        
            //Act
            var result = await _tradeService.SellEquity(traderTransaction);

            //Assert
            Assert.Equal("Can't sell as transaction units are less than holding units", result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_SellEquity_SuccessfullySold_BrokerageChargedMoreThanMin20()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 2, TraderId = 1, TransactionUnits = 200 };
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2, UnitPrice = 1000 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 400000 };
            var traderHoldings = new Data.Entities.TraderHolding() { EquityId = 2, TraderId = 1, UnitHoldings = 200 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.AddTraderTransaction(It.IsAny<Data.Entities.TraderTransaction>()));
            _mockTradeRepository.Setup(x => x.GetTraderHoldingsByEquityTraderId(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(traderHoldings);
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.UpdateTraderHoldings(It.IsAny<Data.Entities.TraderHolding>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.SellEquity(traderTransaction);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_SellEquity_SuccessfullySold_MinBrokerageCharged()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 2, TraderId = 1, TransactionUnits = 9 };
            Data.Entities.Equity equityInfo = new Data.Entities.Equity() { Id = 2, UnitPrice = 200 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };
            var traderHoldings = new Data.Entities.TraderHolding() { EquityId = 2, TraderId = 1, UnitHoldings = 10 };
            _mockTradeRepository.Setup(x => x.GetEquityById(It.IsAny<int>())).ReturnsAsync(equityInfo);
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.AddTraderTransaction(It.IsAny<Data.Entities.TraderTransaction>()));
            _mockTradeRepository.Setup(x => x.GetTraderHoldingsByEquityTraderId(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(traderHoldings);
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.UpdateTraderHoldings(It.IsAny<Data.Entities.TraderHolding>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.SellEquity(traderTransaction);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_AddFund_InvalidTrader()
        {
            //Arrange
            TraderFund traderFund = new TraderFund();
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync((Data.Entities.Trader)null);

            //Act
            var result = await _tradeService.AddFunds(traderFund);

            //Assert
            Assert.Equal("Invalid Trader", result);
        }

        [Fact]
        public async Task TradeService_AddFund_SuccessfullyAdded()
        {
            //Arrange
            TraderFund traderFund = new TraderFund() { Id = 1, Funds = 12144 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.AddFunds(traderFund);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }

        [Fact]
        public async Task TradeService_AddFund_SuccessfullyAdded_MoreThan1Lakh()
        {
            //Arrange
            TraderFund traderFund = new TraderFund() { Id = 1, Funds = 90144 };
            Data.Entities.Trader traderInfo = new Data.Entities.Trader() { Id = 1, Funds = 40000 };
            _mockTradeRepository.Setup(x => x.GetTraderById(It.IsAny<int>())).ReturnsAsync(traderInfo);
            _mockTradeRepository.Setup(x => x.UpdateTrader(It.IsAny<Data.Entities.Trader>()));
            _mockTradeRepository.Setup(x => x.Complete());

            //Act
            var result = await _tradeService.AddFunds(traderFund);

            //Assert
            Assert.Null(result);
            _mockTradeRepository.VerifyAll();
        }
    }
}
