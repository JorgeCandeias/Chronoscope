using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TrackingScopeTests
    {
        [Fact]
        public void ThrowsOnNullContext()
        {
            // arrange
            var options = Options.Create(new ChronoscopeOptions());
            IChronoscopeContext? context = null;
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var parentId = Guid.NewGuid();

            // act
            var result = Assert.Throws<ArgumentNullException>(() => new TrackingScope(options, context, id, name, parentId));

            // assert
            Assert.Equal(nameof(context), result.ParamName);
        }
    }
}