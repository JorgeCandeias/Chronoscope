using Moq;
using System;
using System.Threading;
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
            IAutoTracker tracker = Mock.Of<IAutoTracker>();
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
            IAutoTracker tracker = Mock.Of<IAutoTracker>();

            // act
            tracker.Track(scope => { });

            // assert
            Mock.Get(tracker).Verify(x => x.Track(It.IsAny<Action<ITrackingScope, CancellationToken>>(), It.IsAny<CancellationToken>()));
        }
    }
}