using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeManualTrackerExtensionsTests
    {
        [Fact]
        public void CreateManualTrackerThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.CreateManualTracker());

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void CreateManualTrackerWorks()
        {
            // arrange
            var tracker = Mock.Of<IManualTracker>();
            var scope = Mock.Of<ITrackingScope>(x => x.CreateManualTracker(It.IsAny<Guid>()) == tracker);

            // act
            var result = scope.CreateManualTracker();

            // assert
            Mock.Get(scope).VerifyAll();
            Assert.Same(tracker, result);
        }

        [Fact]
        public void StartManualTrackerWithIdThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            var id = Guid.NewGuid();

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.StartManualTracker(id));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void StartManualTrackerWithIdWorks()
        {
            // arrange
            var id = Guid.NewGuid();
            var tracker = Mock.Of<IManualTracker>();
            var scope = Mock.Of<ITrackingScope>(x => x.CreateManualTracker(id) == tracker);

            // act
            var result = scope.StartManualTracker(id);

            // assert
            Mock.Get(scope).VerifyAll();
            Mock.Get(tracker).Verify(x => x.Start());
            Assert.Same(tracker, result);
        }

        [Fact]
        public void StartManualTrackerThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.StartManualTracker());

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void StartManualTrackerWorks()
        {
            // arrange
            var tracker = Mock.Of<IManualTracker>();
            var scope = Mock.Of<ITrackingScope>(x => x.CreateManualTracker(It.IsAny<Guid>()) == tracker);

            // act
            var result = scope.StartManualTracker();

            // assert
            Mock.Get(scope).VerifyAll();
            Mock.Get(tracker).Verify(x => x.Start());
            Assert.Same(tracker, result);
        }
    }
}