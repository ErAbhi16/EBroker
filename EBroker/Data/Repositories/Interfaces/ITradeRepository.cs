using EBroker.Data.Entities;
using System.Threading.Tasks;

namespace EBroker.Data.Repositories.Interfaces
{
    public interface ITradeRepository
    {
        Task<Equity> GetEquityById(int equityId);

        Task<Trader> GetTraderById(int traderId);

        Task AddTraderTransaction(TraderTransaction traderTransaction);

        Task<TraderHolding> GetTraderHoldingsByEquityTraderId(int equityId, int traderId);

        Task AddTraderHoldings(TraderHolding traderHolding);

        Task UpdateTraderHoldings(TraderHolding traderHolding);

        Task UpdateTrader(Trader trader);

        Task Complete();

    }
}
