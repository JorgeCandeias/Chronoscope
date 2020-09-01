using Chronoscope.Tests.Fakes;
using Moq;
using System;
using System.Threading;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeAutoTrackerExtensionsTests
    {
        [Fact]
        public void TrackWithIdAndScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            var id = Guid.NewGuid();
            Action<ITrackingScope, CancellationToken> workload = null;
            var token = CancellationToken.None;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(id, workload, token));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWithIdAndScopeAndTokenWorks()
        {
            // arrange
            var id = Guid.NewGuid();
            var tracker = Mock.Of<IAutoTracker>();
            ITrackingScope scope = Mock.Of<ITrackingScope>(x => x.CreateAutoTracker(id) == tracker);
            Action<ITrackingScope, CancellationToken> workload = null;
            var token = CancellationToken.None;

            // act
            scope.Track(id, workload, token);

            // assert
            Mock.Get(scope).VerifyAll();
            Mock.Get(tracker).Verify(x => x.Track(workload, token));
        }

        [Fact]
        public void TrackWithIdAndScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            var id = Guid.NewGuid();
            Action<ITrackingScope> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(id, workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWithIdAndScopeWorks()
        {
            // arrange
            var id = Guid.NewGuid();
            var scope = new FakeTrackingScope();
            var called = false;
            void workload(ITrackingScope scope) { called = true; }

            // act
            scope.Track(id, workload);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackWithIdThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            var id = Guid.NewGuid();
            Action workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(id, workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWithIdWorks()
        {
            // arrange
            var id = Guid.NewGuid();
            var scope = new FakeTrackingScope();
            var called = false;
            void workload() { called = true; }

            // act
            scope.Track(id, workload);

            // assert
            Assert.True(called);
        }
    }
}