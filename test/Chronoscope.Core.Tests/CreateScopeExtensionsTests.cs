using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class CreateScopeExtensionsTests
    {
        [Fact]
        public void CreateScopeWithNameThrowsOnNullCreator()
        {
            // arrange
            var name = Guid.NewGuid().ToString();
            ICreateScope creator = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => creator.CreateScope(name));

            // assert
            Assert.Equal(nameof(creator), ex.ParamName);
        }

        [Fact]
        public void CreateScopeWithNameReturnsNewScope()
        {
            // arrange
            var name = Guid.NewGuid().ToString();
            var scope = Mock.Of<ITrackingScope>();
            var creator = Mock.Of<ICreateScope>(x => x.CreateScope(It.IsNotIn(Guid.Empty), name) == scope);

            // act
            var result = creator.CreateScope(name);

            // assert
            Assert.Same(scope, result);
            Mock.Get(creator).VerifyAll();
        }

        [Fact]
        public void CreateScopeWithoutParamsReturnsNewScope()
        {
            // arrange
            var scope = Mock.Of<ITrackingScope>();
            var creator = Mock.Of<ICreateScope>(x => x.CreateScope(It.IsNotIn(Guid.Empty), null) == scope);

            // act
            var result = creator.CreateScope();

            // assert
            Assert.Same(scope, result);
            Mock.Get(creator).VerifyAll();
        }
    }
}