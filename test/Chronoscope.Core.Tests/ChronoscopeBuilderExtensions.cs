using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ChronoscopeBuilderExtensions
    {
        [Fact]
        public void ConstructorThrowsOnNullBuilder()
        {
            // arrange
            IHostBuilder builder = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new ChronoscopeBuilder(builder));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void ConfigureChronoscopeThrowsOnNullAction()
        {
            // arrange
            var builder = Mock.Of<IHostBuilder>();
            var cbuilder = new ChronoscopeBuilder(builder);
            Action<HostBuilderContext, IChronoscopeBuilder> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => cbuilder.ConfigureChronoscope(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }

        [Fact]
        public void ConfigureServicesThrowsOnNullAction()
        {
            // arrange
            var builder = Mock.Of<IHostBuilder>();
            var cbuilder = new ChronoscopeBuilder(builder);
            Action<HostBuilderContext, IServiceCollection> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => cbuilder.ConfigureServices(configure));

            // assert
            Assert.Equal(nameof(configure), ex.ParamName);
        }
    }
}