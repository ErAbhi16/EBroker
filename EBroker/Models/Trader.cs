using System.Diagnostics.CodeAnalysis;

namespace EBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class Trader
    {
        public int TraderId { get; set; }

        public double TraderFunds { get; set; }
    }
}
