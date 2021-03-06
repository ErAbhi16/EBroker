using EBroker.Data.Repositories.Interfaces;
using EBroker.Data.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace EBroker.Data.Repositories
{
    public class TradeRepository : ITradeRepository
    {
        private readonly EBrokerContext _dbContext;
        public TradeRepository(EBrokerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Equity> GetEquityById(int equityId)
        {
            return await _dbContext.Equity.AsNoTracking().FirstOrDefaultAsync(x => x.Id == equityId);
        }

        public async Task<Trader> GetTraderById(int traderId)
        {
            return await _dbContext.Trader.AsNoTracking().FirstOrDefaultAsync(x => x.Id == traderId);
        }

        public async Task AddTraderTransaction(TraderTransaction traderTransaction)
        {
            await _dbContext.TraderTransaction.AddAsync(traderTransaction);
        }

        public async Task<TraderHolding> GetTraderHoldingsByEquityTraderId(int equityId, int traderId)
        {
            return await _dbContext.TraderHolding.AsNoTracking().FirstOrDefaultAsync(x => x.EquityId == equityId && x.TraderId == traderId);
        }

        public async Task AddTraderHoldings(TraderHolding traderHolding)
        {
            await _dbContext.TraderHolding.AddAsync(traderHolding);
        }

        public async Task UpdateTraderHoldings(TraderHolding traderHolding)
        {
            var entry = _dbContext.TraderHolding.Find(traderHolding.TraderId, traderHolding.EquityId);
            _dbContext.Entry(entry).CurrentValues.SetValues(traderHolding);
        }

        public async Task UpdateTrader(Trader trader)
        {
            var entry = _dbContext.Trader.Find(trader.Id);
            _dbContext.Entry(entry).CurrentValues.SetValues(trader);
        }

        public async Task Complete()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
