using EBroker.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EBroker.UnitTests
{
    public class TradeHelperTest
    {
        [Fact]
        public void TradeHelper_IsValidTime_ReturnsTrue()
        {
            DateTime dateTime = new DateTime(2021, 12, 23, 11, 23, 12);
            var result=TradeHelper.IsValidTransactionTime(dateTime);
            Assert.True(result);
        }

        [Fact]
        public void TradeHelper_IsValidTime_ReturnsFalse()
        {
            DateTime dateTime = new DateTime(2021, 12, 23, 08, 23, 12);
            var result = TradeHelper.IsValidTransactionTime(dateTime);
            Assert.False(result);
        }

    }
}
