using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeTests
    {
        [Fact]
        public void CreatesScope()
        {
            // arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var scope = Mock.Of<ITrackingScope>(x => x.Id == id && x.Name == name);
            var factory = Mock.Of<ITrackingScopeFactory>(x => x.CreateScope(id, name, null) == scope);
            var context = Mock.Of<IChronoscopeContext>(x => x.ScopeFactory == factory);
            var monitor = new Chronoscope(context);

            // act
            var result = monitor.CreateScope(id, name);

            // assert
            Assert.Same(scope, result);
        }
    }
}