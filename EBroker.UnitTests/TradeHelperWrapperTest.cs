using EBroker.Utils.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace EBroker.UnitTests
{
    public class TradeHelperWrapperTest
    {
        private readonly TradeHelperWrapper _tradeHelperWrapper;
        public TradeHelperWrapperTest()
        {
            _tradeHelperWrapper = new TradeHelperWrapper();
        }
        [Fact]
        public void TradeHelperWrapper_IsValidTime_ReturnsTrue()
        {
            DateTime dateTime = new DateTime(2021, 12, 23, 11, 23, 12);
            var result = _tradeHelperWrapper.IsValidTransactionTime(dateTime);
            Assert.True(result);
        }

        [Fact]
        public void TradeHelperWrapper_IsValidTime_ReturnsFalse()
        {
            DateTime dateTime = new DateTime(2021, 12, 23, 08, 23, 12);
            var result = _tradeHelperWrapper.IsValidTransactionTime(dateTime);
            Assert.False(result);
        }
    }
}
