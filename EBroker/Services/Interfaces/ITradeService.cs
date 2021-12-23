using EBroker.Models;
using System.Threading.Tasks;

namespace EBroker.Services.Interfaces
{
    public interface ITradeService
    {
        Task<string> BuyEquity(TraderTransaction traderTransaction);

        Task<string> AddFunds(TraderFund traderFund);

        Task<string> SellEquity(TraderTransaction traderTransaction);
    }
}
