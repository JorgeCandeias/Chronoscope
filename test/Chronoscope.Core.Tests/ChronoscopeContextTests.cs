using Chronoscope.Events;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeContextTests
    {
        [Fact]
        public void ConstructorThrowsOnNullClock()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(null, null, null, null, null, null));

            // assert
            Assert.Equal("clock", ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullSink()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(Mock.Of<ISystemClock>(), null, null, null, null, null));

            // assert
            Assert.Equal("sink", ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullEventFactory()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(Mock.Of<ISystemClock>(), Mock.Of<ITrackingSinks>(), null, null, null, null));

            // assert
            Assert.Equal("eventFactory", ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullStopwatchFactory()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(Mock.Of<ISystemClock>(), Mock.Of<ITrackingSinks>(), Mock.Of<ITrackingEventFactory>(), null, null, null));

            // assert
            Assert.Equal("stopwatchFactory", ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullScopeFactory()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(Mock.Of<ISystemClock>(), Mock.Of<ITrackingSinks>(), Mock.Of<ITrackingEventFactory>(), Mock.Of<ITrackerStopwatchFactory>(), null, null));

            // assert
            Assert.Equal("scopeFactory", ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullTrackerFactory()
        {
            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeContext(Mock.Of<ISystemClock>(), Mock.Of<ITrackingSinks>(), Mock.Of<ITrackingEventFactory>(), Mock.Of<ITrackerStopwatchFactory>(), Mock.Of<ITrackingScopeFactory>(), null));

            // assert
            Assert.Equal("trackerFactory", ex.ParamName);
        }

        [Fact]
        public void ConstructorWorks()
        {
            var clock = Mock.Of<ISystemClock>();
            var sinks = Mock.Of<ITrackingSinks>();
            var eventFactory = Mock.Of<ITrackingEventFactory>();
            var stopwatchFactory = Mock.Of<ITrackerStopwatchFactory>();
            var scopeFactory = Mock.Of<ITrackingScopeFactory>();
            var trackerFactory = Mock.Of<ITrackerFactory>();

            // act
            var context = new ChronoscopeContext(clock, sinks, eventFactory, stopwatchFactory, scopeFactory, trackerFactory);

            // assert
            Assert.Same(clock, context.Clock);
            Assert.Same(sinks, context.Sink);
            Assert.Same(eventFactory, context.EventFactory);
            Assert.Same(stopwatchFactory, context.StopwatchFactory);
            Assert.Same(scopeFactory, context.ScopeFactory);
            Assert.Same(trackerFactory, context.TrackerFactory);
        }
    }
}