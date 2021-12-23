namespace EBroker.Utils.Helpers
{
    public class TradeHelperWrapper : ITradeHelperWrapper
    {
        public bool IsValidTransactionTime()
        {
            return TradeHelper.IsValidTransactionTime();
        }
    }
}
