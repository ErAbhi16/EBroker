using System.Diagnostics.CodeAnalysis;

namespace EBroker.Models
{
    [ExcludeFromCodeCoverage]
    public class TraderHolding
    {
        public int EquityId { get; set; }

        public int TraderId { get; set; }

        public int UnitHoldings { get; set; }
    }
}
