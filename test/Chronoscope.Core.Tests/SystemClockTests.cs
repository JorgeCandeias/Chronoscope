using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class SystemClockTests
    {
        [Fact]
        public void NowIsNow()
        {
            // arrange
            var clock = new SystemClock();
            var now = DateTimeOffset.Now;

            // act
            var result = clock.Now;

            // assert
            Assert.True(result > now.AddMilliseconds(-1));
            Assert.True(result < now.AddMilliseconds(1));
        }
    }
}