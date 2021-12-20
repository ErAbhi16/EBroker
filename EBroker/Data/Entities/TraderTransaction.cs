using EBroker.Models;
using System.ComponentModel.DataAnnotations;

namespace EBroker.Data.Entities
{
    public class TraderTransaction
    {
        [Key]
        public int Id { get; set; }
        public int EquityId { get; set; }

        public int TraderId { get; set; }

        public int TransactionUnits { get; set; }

        public TradeAction TraderAction { get; set; }
    }
}
