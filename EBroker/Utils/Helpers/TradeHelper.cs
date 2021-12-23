using System;

namespace EBroker.Utils.Helpers
{
    public static class TradeHelper
    {
        public static bool IsValidTransactionTime(DateTime? dateTime)
        {
            if(dateTime == null)
            dateTime = DateTime.Now;
            if((dateTime.Value.Hour >= 9 && dateTime.Value.Hour < 15)
                || (dateTime.Value.Hour == 15 && dateTime.Value.Minute == 0 && dateTime.Value.Second ==0) 
                && dateTime.Value.DayOfWeek != DayOfWeek.Saturday
                && dateTime.Value.DayOfWeek != DayOfWeek.Sunday)
            return true;
            return false;
        }
    }
}
