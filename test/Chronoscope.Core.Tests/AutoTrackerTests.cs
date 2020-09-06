using Chronoscope.Events;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class AutoTrackerTests
    {
        [Fact]
        public void VoidTrackThrowsOnNullWorkload()
        {
            // arrange
            var context = Mock.Of<IChronoscopeContext>(x =>
                x.StopwatchFactory == Mock.Of<ITrackerStopwatchFactory>(x =>
                    x.Create() == Mock.Of<ITrackerStopwatch>()) &&
                x.Sink == Mock.Of<ITrackingSinks>() &&
                x.EventFactory == Mock.Of<ITrackingEventFactory>() &&
                x.Clock == Mock.Of<ISystemClock>());
            var id = Guid.NewGuid();
            var scope = Mock.Of<ITrackingScope>();
            Action<ITrackingScope, CancellationToken> workload = null;
            var tracker = new AutoTracker(context, id, scope);

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload, default));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void VoidTrackCallsWorkload()
        {
            // arrange
            var called = false;
            var context = Mock.Of<IChronoscopeContext>(x =>
                x.StopwatchFactory == Mock.Of<ITrackerStopwatchFactory>(x =>
                    x.Create() == Mock.Of<ITrackerStopwatch>()) &&
                x.Sink == Mock.Of<ITrackingSinks>() &&
                x.EventFactory == Mock.Of<ITrackingEventFactory>() &&
                x.Clock == Mock.Of<ISystemClock>());
            var id = Guid.NewGuid();
            var scope = Mock.Of<ITrackingScope>();
            void workload(ITrackingScope scope, CancellationToken token) { called = true; }
            var tracker = new AutoTracker(context, id, scope);

            // act
            tracker.Track(workload, default);

            // assert
            Assert.True(called);
        }
    }
}