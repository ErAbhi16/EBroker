using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EBroker.Data.Entities
{
    public class Trader
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public decimal Funds { get; set; }

        public IEnumerable<TraderTransaction> TradeTransactions { get; set; }

        public IEnumerable<TraderHolding> TradeHoldings { get; set; }
    }
}
