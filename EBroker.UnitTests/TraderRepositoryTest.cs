using EBroker.Data;
using EBroker.Data.Entities;
using EBroker.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace EBroker.UnitTests
{
    /// <summary>
    /// System Under Test
    /// Trade Repository
    /// </summary>
    public class TradeRepositoryTest : IClassFixture<EBrokerSeedDataFixture>
    {
        private EBrokerSeedDataFixture _fixture;

        private TradeRepository tradeRepository;
        public TradeRepositoryTest(EBrokerSeedDataFixture fixture)
        {
            _fixture = fixture;
            tradeRepository = new TradeRepository(_fixture.context);
        }

        [Fact]
        public async Task TradeRepository_GetEquityById_EquityDoesnotExists()
        {
            //Arrange
            int equityId = 90;
            //Act
            var result = await tradeRepository.GetEquityById(equityId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TradeRepository_GetEquityById_EquityExists()
        {
            //Arrange
            int equityId = 2;
            //Act
            var result = await tradeRepository.GetEquityById(equityId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderById_Null()
        {
            //Arrange
            int traderId = 29;
            //Act
            var result = await tradeRepository.GetTraderById(traderId);
            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderById_NotNull()
        {
            //Arrange
            int traderId = 2;
            //Act
            var result = await tradeRepository.GetTraderById(traderId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderHoldingsByEquityTraderId_NotNull()
        {
            //Arrange
            int traderId = 1; int equityId = 2;
            //Act
            var result = await tradeRepository.GetTraderHoldingsByEquityTraderId(equityId, traderId);
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task TradeRepository_GetTraderHoldingsByEquityTraderId_Null()
        {
            //Arrange
            int traderId = 6; int equityId = 6;
            //Act
            var result = await tradeRepository.GetTraderHoldingsByEquityTraderId(equityId, traderId);

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task TradeRepository_AddTraderTransaction_SuccessfullyforBuy()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 1, TraderId = 2, Id = 12, TraderAction = Models.TradeAction.Buy, TransactionUnits = 3 };
            //Act
            await tradeRepository.AddTraderTransaction(traderTransaction);
            await _fixture.context.SaveChangesAsync();

            //Assert
            var objectAdded = await _fixture.context.TraderTransaction.FirstAsync(x => x.EquityId == traderTransaction.EquityId && x.TraderId == traderTransaction.TraderId);
            Assert.NotNull(objectAdded);
            _fixture.context.TraderTransaction.Remove(objectAdded);
            await _fixture.context.SaveChangesAsync();
        }

        [Fact]
        public async Task TradeRepository_AddTraderTransaction_SuccessfullyforSell()
        {
            //Arrange
            TraderTransaction traderTransaction = new TraderTransaction() { EquityId = 1, TraderId = 2, Id = 12, TraderAction = Models.TradeAction.Sell, TransactionUnits = 3 };
            //Act
            await tradeRepository.AddTraderTransaction(traderTransaction);
            await _fixture.context.SaveChangesAsync();

            //Assert
            var objectAdded = await _fixture.context.TraderTransaction.FirstAsync(x => x.EquityId == traderTransaction.EquityId && x.TraderId == traderTransaction.TraderId);
            Assert.NotNull(objectAdded);
            _fixture.context.TraderTransaction.Remove(objectAdded);
            await _fixture.context.SaveChangesAsync();
        }

        [Fact]
        public async Task TradeRepository_AddTraderHoldings_Success()
        {
            //Arrange
            TraderHolding traderHolding = new TraderHolding() { EquityId = 2, TraderId = 2, UnitHoldings = 12 };
            //Act
            await tradeRepository.AddTraderHoldings(traderHolding);
            await _fixture.context.SaveChangesAsync();

            //Assert
            var objectAdded = await _fixture.context.TraderHolding.FirstAsync(x => x.EquityId == traderHolding.EquityId && x.TraderId == traderHolding.TraderId);
            Assert.NotNull(objectAdded);
            _fixture.context.TraderHolding.Remove(objectAdded);
            await _fixture.context.SaveChangesAsync();
        }

        [Fact]
        public async Task TradeRepository_UpdateTraderHoldings_Success()
        {
            //Arrange
            TraderHolding traderHolding = new TraderHolding() { EquityId = 2, TraderId = 1, UnitHoldings = 12 };
            //Act
            await tradeRepository.UpdateTraderHoldings(traderHolding);
            await _fixture.context.SaveChangesAsync();

            //Assert
            var objectAdded = await _fixture.context.TraderHolding.FirstAsync(x => x.EquityId == traderHolding.EquityId && x.TraderId == traderHolding.TraderId);
            Assert.NotNull(objectAdded);
            Assert.Equal(12, objectAdded.UnitHoldings);
        }

        [Fact]
        public async Task TradeRepository_UpdateTrader_Success()
        {
            //Arrange
            Trader traderDetails = new Trader() { Id = 2, Funds = 2000, Name = "Ram" };
            //Act
            await tradeRepository.UpdateTrader(traderDetails);
            await _fixture.context.SaveChangesAsync();

            //Assert
            var objectAdded = await _fixture.context.Trader.FirstAsync(x => x.Id == traderDetails.Id);
            Assert.NotNull(objectAdded);
            Assert.Equal(2000, objectAdded.Funds);
        }

        [Fact]
        public async Task TradeRepository_Complete_Success()
        {
            await tradeRepository.Complete();
        }
    }
}
