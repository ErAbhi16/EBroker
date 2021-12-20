namespace EBroker.Models
{
    public class TraderTransaction
    {
        public int EquityId { get; set; }

        public int TraderId { get; set; }

        public int TransactionUnits { get; set; }

        public TradeAction TraderAction { get; set; }
    }
}
