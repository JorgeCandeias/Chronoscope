using Chronoscope.Events;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
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

        [Fact]
        public void VoidTrackCancelsWorkload()
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
            void workload(ITrackingScope scope, CancellationToken token) { token.ThrowIfCancellationRequested(); called = true; }
            var tracker = new AutoTracker(context, id, scope);

            // act
            Assert.Throws<OperationCanceledException>(() =>
            {
                tracker.Track(workload, new CancellationToken(true));
            });

            // assert
            Assert.False(called);
        }

        [Fact]
        public void VoidTrackFaultsWorkload()
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
            static void workload(ITrackingScope scope, CancellationToken token) { throw new InvalidOperationException("Test"); }
            var tracker = new AutoTracker(context, id, scope);

            // act
            var ex = Assert.Throws<InvalidOperationException>(() =>
            {
                tracker.Track(workload, default);
            });

            // assert
            Assert.Equal("Test", ex.Message);
        }

        [Fact]
        public void ResultTrackThrowsOnNullWorkload()
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
            Func<ITrackingScope, CancellationToken, int> workload = null;
            var tracker = new AutoTracker(context, id, scope);

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload, default));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncThrowsOnNullWorkload()
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
            Func<ITrackingScope, CancellationToken, Task> workload = null;
            var tracker = new AutoTracker(context, id, scope);

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => tracker.TrackAsync(workload, default)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncCallsWorkload()
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
            Task workload(ITrackingScope scope, CancellationToken token) { called = true; return Task.CompletedTask; }
            var tracker = new AutoTracker(context, id, scope);

            // act
            await tracker.TrackAsync(workload, default).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncCancelsWorkload()
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
            Task workload(ITrackingScope scope, CancellationToken token) { token.ThrowIfCancellationRequested(); called = true; return Task.CompletedTask; }
            var tracker = new AutoTracker(context, id, scope);

            // act
            await Assert.ThrowsAsync<OperationCanceledException>(async () =>
            {
                await tracker.TrackAsync(workload, new CancellationToken(true)).ConfigureAwait(false);
            }).ConfigureAwait(false);

            // assert
            Assert.False(called);
        }

        [Fact]
        public async Task VoidTrackAsyncFaultsWorkload()
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
            static Task workload(ITrackingScope scope, CancellationToken token) { throw new InvalidOperationException("Test"); }
            var tracker = new AutoTracker(context, id, scope);

            // act
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => tracker.TrackAsync(workload, default)).ConfigureAwait(false);

            // assert
            Assert.Equal("Test", ex.Message);
        }
    }
}