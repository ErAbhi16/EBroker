using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBroker.Data.Entities
{
    public class Equity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public IEnumerable<TraderTransaction> TradeTransactions { get; set; }

        public IEnumerable<TraderHolding> TradeHoldings { get; set; }
    }
}
