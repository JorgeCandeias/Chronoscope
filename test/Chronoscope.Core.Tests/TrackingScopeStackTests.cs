using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeStackTests
    {
        [Fact]
        public async Task HandlesHierarchy()
        {
            // arrange
            var stack = new TrackingScopeStack();
            var scope1 = Mock.Of<ITrackingScope>();
            var scope21 = Mock.Of<ITrackingScope>();
            var scope22 = Mock.Of<ITrackingScope>();
            var scope31 = Mock.Of<ITrackingScope>();
            var scope32 = Mock.Of<ITrackingScope>();
            var scope33 = Mock.Of<ITrackingScope>();

            // assert
            Assert.Empty(stack.PeekAll());

            // act - test one level
            await Task.Run(() =>
            {
                stack.Push(scope1);
                Assert.Collection(stack.PeekAll(), x => Assert.Same(scope1, x));
            }).ConfigureAwait(false);

            // act - test two levels
            await Task.Run(async () =>
            {
                stack.Push(scope21);
                Assert.Collection(stack.PeekAll(), x => Assert.Same(scope21, x));

                await Task.Run(() =>
                {
                    stack.Push(scope22);
                    Assert.Collection(stack.PeekAll(), x => Assert.Same(scope22, x), x => Assert.Same(scope21, x));
                }).ConfigureAwait(false);

                Assert.Collection(stack.PeekAll(), x => Assert.Same(scope21, x));

            }).ConfigureAwait(false);

            // act - test three levels
            await Task.Run(async () =>
            {
                stack.Push(scope31);
                Assert.Collection(stack.PeekAll(), x => Assert.Same(scope31, x));

                await Task.Run(async () =>
                {
                    stack.Push(scope32);
                    Assert.Collection(stack.PeekAll(), x => Assert.Same(scope32, x), x => Assert.Same(scope31, x));

                    await Task
                        .Run(() =>
                        {
                            stack.Push(scope33);
                            Assert.Collection(stack.PeekAll(), x => Assert.Same(scope33, x), x => Assert.Same(scope32, x), x => Assert.Same(scope31, x));
                        })
                        .ConfigureAwait(false);

                    Assert.Collection(stack.PeekAll(), x => Assert.Same(scope32, x), x => Assert.Same(scope31, x));

                }).ConfigureAwait(false);

                Assert.Collection(stack.PeekAll(), x => Assert.Same(scope31, x));

            }).ConfigureAwait(false);

            // assert
            Assert.Empty(stack.PeekAll());
        }
    }
}