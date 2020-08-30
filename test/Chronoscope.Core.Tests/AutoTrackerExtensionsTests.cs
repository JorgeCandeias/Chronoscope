using Chronoscope.Tests.Fakes;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class AutoTrackerExtensionsTests
    {
        [Fact]
        public void TrackWorkloadThrowsOnNullTracker()
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
        public void TrackWorkloadThrowsOnNullWorkload()
        {
            // arrange
            var tracker = new FakeAutoTracker();
            Action<ITrackingScope> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => tracker.Track(workload));

            // assert
            Assert.Equal(nameof(workload), ex.ParamName);
        }

        [Fact]
        public void TrackWorkloadWorks()
        {
            // arrange
            var tracker = new FakeAutoTracker();
            var called = false;

            // act
            tracker.Track(scope => { called = true; });

            // assert
            Assert.True(called);
        }
    }
}