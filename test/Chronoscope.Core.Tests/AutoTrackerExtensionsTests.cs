using Chronoscope.Tests.Fakes;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class AutoTrackerExtensionsTests
    {
        [Fact]
        public void TrackWorkloadWithScopeThrowsOnNullTracker()
        {
            // arrange
            IAutoTracker tracker = null;
            Action<ITrackingScope> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(tracker), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithScopeThrowsOnNullWorkload()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            Action<ITrackingScope> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithScopeWorks()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            var called = false;
            void workload(ITrackingScope scope) { called = true; }

            // act
            tracker.Track(workload);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackWorkloadThrowsOnNullTracker()
        {
            // arrange
            IAutoTracker tracker = null;
            Action workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(tracker), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadThrowsOnNullWorkload()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            Action workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWorks()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            var called = false;
            void workload() { called = true; }

            // act
            tracker.Track(workload);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackWorkloadWithScopeAndResultThrowsOnNullTracker()
        {
            // arrange
            IAutoTracker tracker = null;
            Func<ITrackingScope, int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(tracker), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithScopeAndResultThrowsOnNullWorkload()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            Func<ITrackingScope, int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithScopeAndResultWorks()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            static int workload(ITrackingScope scope) { return 123; }

            // act
            var result = tracker.Track(workload);

            // assert
            Assert.Equal(123, result);
        }

        [Fact]
        public void TrackWorkloadWithResultThrowsOnNullTracker()
        {
            // arrange
            IAutoTracker tracker = null;
            Func<int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(tracker), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithResultThrowsOnNullWorkload()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            Func<int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWithResultWorks()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            static int workload() { return 123; }

            // act
            var result = tracker.Track(workload);

            // assert
            Assert.Equal(123, result);
        }

        [Fact]
        public async Task TrackAsyncWorkloadWithScopeThrowsOnNullTracker()
        {
            // arrange
            IAutoTracker tracker = null;
            Func<ITrackingScope, Task> workload = null;

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => tracker.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(tracker), ex.ParamName);
        }

        [Fact]
        public async Task TrackAsyncWorkloadWithScopeThrowsOnNullWorkload()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            Func<ITrackingScope, Task> workload = null;

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => tracker.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public async Task TrackAsyncWorkloadWithScopeWorks()
        {
            // arrange
            IAutoTracker tracker = new FakeAutoTracker();
            var called = false;
            Task workload(ITrackingScope scope) { called = true; return Task.CompletedTask; }

            // act
            await tracker.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }
    }
}