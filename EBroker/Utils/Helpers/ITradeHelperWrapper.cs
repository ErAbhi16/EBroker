using System;

namespace EBroker.Utils.Helpers
{
    public interface ITradeHelperWrapper
    {
        bool IsValidTransactionTime(DateTime? dateTime = null);
    }
}
