using EBroker.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EBroker.UnitTests
{
    public class TradeHelperTest
    {
        [Theory,MemberData(nameof(ValidateDateTimeData))]
        public void TradeHelper_CheckIfValidTimeWindow(int yr,int month,int day,int hr,int min, int sec, bool isValid )
        {
            DateTime dateTime = new DateTime(yr, month, day, hr, min, sec);
            var result = TradeHelper.IsValidTransactionTime(dateTime);
            Assert.Equal(result,isValid);
        }

        public static IEnumerable<object[]> ValidateDateTimeData =>
        new List<object[]>
        {
            new object[] { 2021, 12, 23, 22, 2, 12, false  },
            new object[] { 2021, 12, 25, 22, 2, 12, false  },
            new object[] { 2021, 12, 26, 22, 2, 12, false  },
            new object[] { 2021, 12, 23, 11, 2, 12, true  },
            new object[] { 2021, 12, 23, 15, 0, 0, true  }
        };

    }
}
