using Chronoscope.Core.Tests.Fakes;
using Chronoscope.Events;
using Chronoscope.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingSinksTests
    {
        [Fact]
        public void ConstructorsThrowsOnNullLogger()
        {
            // arrange
            ILogger<TrackingSinks> logger = null;
            IEnumerable<ITrackingSink> sinks = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new TrackingSinks(logger, sinks));

            // assert
            Assert.Equal(nameof(logger), ex.ParamName);
        }

        [Fact]
        public void ConstructorsThrowsOnNullSinks()
        {
            // arrange
            ILogger<TrackingSinks> logger = new FakeLogger<TrackingSinks>();
            IEnumerable<ITrackingSink> sinks = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new TrackingSinks(logger, sinks));

            // assert
            Assert.Equal(nameof(sinks), ex.ParamName);
        }

        [Fact]
        public void SinkHandlesException()
        {
            // arrange
            var logger = new FakeLogger<TrackingSinks>();
            var evt = new FakeTrackingEvent();
            var ex = new InvalidTimeZoneException();
            var sink = new FakeFaultingTrackingSink(ex);
            var sinks = Enumerable.Repeat(sink, 1);
            var obj = new TrackingSinks(logger, sinks);

            // act
            var rex = Assert.Throws<InvalidTimeZoneException>(() => obj.Sink(evt));

            // assert
            Assert.Same(ex, rex);
            Assert.Collection(logger.Items, x =>
            {
                Assert.Equal(999, x.EventId.Id);
                Assert.Equal("SinkFailed", x.EventId.Name);
                Assert.Equal(ex, x.Exception);
                Assert.Equal(LogLevel.Error, x.LogLevel);
            });
        }
    }
}