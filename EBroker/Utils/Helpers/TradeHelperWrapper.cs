using System;

namespace EBroker.Utils.Helpers
{
    public class TradeHelperWrapper : ITradeHelperWrapper
    {
        public bool IsValidTransactionTime(DateTime? dateTime = null)
        {
            return TradeHelper.IsValidTransactionTime(dateTime);
        }
    }
}
