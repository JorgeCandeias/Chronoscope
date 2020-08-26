using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackerStopwatchFactoryTests
    {
        [Fact]
        public void CreateReturnsNewInstance()
        {
            // arrange
            var factory = new TrackerStopwatchFactory();

            // act
            var result1 = factory.Create();
            var result2 = factory.Create();

            // assert
            Assert.NotNull(result1);
            Assert.NotNull(result2);
            Assert.NotSame(result1, result2);
        }
    }
}