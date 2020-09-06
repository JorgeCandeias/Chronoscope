using Chronoscope.Tests.Fakes;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeAutoTrackerExtensionsTests
    {
        [Fact]
        public void VoidTrackWithIdAndScopeAndTokenThrowsOnNullScope()
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
        public void VoidTrackWithIdAndScopeAndTokenWorks()
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
        public void VoidTrackWithIdAndScopeThrowsOnNullScope()
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
        public void VoidTrackWithIdAndScopeWorks()
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
        public void VoidTrackWithIdThrowsOnNullScope()
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
        public void VoidTrackWithIdWorks()
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
        public void VoidTrackWithScopeAndTokenThrowsOnNullScope()
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
        public void VoidTrackWithScopeAndTokenWorks()
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
        public void VoidTrackWithScopeThrowsOnNullScope()
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
        public void VoidTrackWithScopeWorks()
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
        public void VoidTrackThrowsOnNullScope()
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
        public void VoidTrackWorks()
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
        public void ResultTrackWithIdAndScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Func<ITrackingScope, CancellationToken, int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(Guid.NewGuid(), workload, CancellationToken.None));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWithIdAndScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload(ITrackingScope scope, CancellationToken token) { called = true; return 123; }

            // act
            var result = scope.Track(Guid.NewGuid(), workload, CancellationToken.None);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public void ResultTrackWithIdAndScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            Func<ITrackingScope, int> workload = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(Guid.NewGuid(), workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWithIdAndScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload(ITrackingScope scope) { called = true; return 123; }

            // act
            var result = scope.Track(Guid.NewGuid(), workload);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public void ResultTrackWithIdThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static int workload() { return 123; }

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(Guid.NewGuid(), workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWithIdWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload() { called = true; return 123; }

            // act
            var result = scope.Track(Guid.NewGuid(), workload);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public void ResultTrackWithScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static int workload(ITrackingScope scope, CancellationToken token) { return 123; }

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWithScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload(ITrackingScope scope, CancellationToken token) { called = true; return 123; }

            // act
            var result = scope.Track(workload);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public void ResultTrackWithScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static int workload(ITrackingScope scope) { return 123; }

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWithScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload(ITrackingScope scope) { called = true; return 123; }

            // act
            var result = scope.Track(workload);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public void ResultTrackThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static int workload() { return 123; }

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => scope.Track(workload));

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public void ResultTrackWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            int workload() { called = true; return 123; }

            // act
            var result = scope.Track(workload);

            // assert
            Assert.True(called);
            Assert.Equal(123, result);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdAndScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload(ITrackingScope scope, CancellationToken token) { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdAndScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload(ITrackingScope scope, CancellationToken token) { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdAndScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload(ITrackingScope scope) { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdAndScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload(ITrackingScope scope) { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload() { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWithIdWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload() { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncWithScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload(ITrackingScope scope, CancellationToken token) { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWithScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload(ITrackingScope scope, CancellationToken token) { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncWithScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload(ITrackingScope scope) { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWithScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload(ITrackingScope scope) { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task VoidTrackAsyncThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task workload() { return Task.CompletedTask; }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task VoidTrackAsyncWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task workload() { called = true; return Task.CompletedTask; }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdAndScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload(ITrackingScope scope, CancellationToken token) { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdAndScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload(ITrackingScope scope, CancellationToken token) { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdAndScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload(ITrackingScope scope) { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdAndScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload(ITrackingScope scope) { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload() { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(Guid.NewGuid(), workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWithIdWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload() { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(Guid.NewGuid(), workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncWithScopeAndTokenThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload(ITrackingScope scope, CancellationToken token) { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWithScopeAndTokenWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload(ITrackingScope scope, CancellationToken token) { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncWithScopeThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload(ITrackingScope scope) { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWithScopeWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload(ITrackingScope scope) { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }

        [Fact]
        public async Task ResultTrackAsyncThrowsOnNullScope()
        {
            // arrange
            ITrackingScope scope = null;
            static Task<int> workload() { return Task.FromResult(123); }

            // act
            var ex = await Assert.ThrowsAsync<ArgumentNullException>(() => scope.TrackAsync(workload)).ConfigureAwait(false);

            // assert
            Assert.Equal(nameof(scope), ex.ParamName);
        }

        [Fact]
        public async Task ResultTrackAsyncWorks()
        {
            // arrange
            var scope = new FakeTrackingScope();
            var called = false;
            Task<int> workload() { called = true; return Task.FromResult(123); }

            // act
            await scope.TrackAsync(workload).ConfigureAwait(false);

            // assert
            Assert.True(called);
        }
    }
}