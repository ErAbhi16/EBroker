using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace EBroker.Data.Entities
{
    [ExcludeFromCodeCoverage]
    public class Equity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public double UnitPrice { get; set; }

        public IEnumerable<TraderTransaction> TradeTransactions { get; set; }

        public IEnumerable<TraderHolding> TradeHoldings { get; set; }
    }
}
