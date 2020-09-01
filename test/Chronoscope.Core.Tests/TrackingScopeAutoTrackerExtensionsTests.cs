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

        [Fact]
        public void TrackWithScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Action<ITrackingScope, CancellationToken> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload, CancellationToken.None));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWithScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            void workload(ITrackingScope scope, CancellationToken token) { called = true; }

            // act
            scope.Track(workload, CancellationToken.None);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackWithScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Action<ITrackingScope> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWithScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            void workload(ITrackingScope scope) { called = true; }

            // act
            scope.Track(workload);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Action workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void TrackWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            void workload() { called = true; }

            // act
            scope.Track(workload);

            // assert
            Assert.True(called);
        }

        [Fact]
        public void TrackWithResultAndIdAndScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Func<ITrackingScope, CancellationToken, int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(Guid.NewGuid(), workload, CancellationToken.None));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }
    }
}