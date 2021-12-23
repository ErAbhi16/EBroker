using System;

namespace EBroker.Utils.Helpers
{
    public static class TradeHelper
    {
        public static bool IsValidTransactionTime()
        {
            var dateTime = DateTime.Now;
            if((dateTime.Hour >= 9 && dateTime.Hour < 15)
                || (dateTime.Hour == 15 && dateTime.Minute ==0 && dateTime.Second ==0) 
                && dateTime.DayOfWeek != DayOfWeek.Saturday
                && dateTime.DayOfWeek != DayOfWeek.Sunday)
            return true;
            return false;
        }
    }
}
