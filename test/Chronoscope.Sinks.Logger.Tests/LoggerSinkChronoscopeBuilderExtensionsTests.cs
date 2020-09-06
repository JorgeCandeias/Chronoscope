using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Sinks.Logger.Tests
{
    public class LoggerSinkChronoscopeBuilderExtensionsTests
    {
        [Fact]
        public void AddLoggerThrowsOnNullBuilder()
        {
            // arrange
            IChronoscopeBuilder builder = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.AddLoggerSink(options => { }));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void AddLoggerThrowsOnNullConfigure()
        {
            // arrange
            IChronoscopeBuilder builder = Mock.Of<IChronoscopeBuilder>();
            Action<LoggerSinkOptions> configure = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => builder.AddLoggerSink(configure));

            // assert
            Assert.Equal(nameof(builder), ex.ParamName);
        }

        [Fact]
        public void AddLoggerSinkWorks()
        {
            // arrange
            var builder = new HostBuilder();
            var chrono = Mock.Of<IChronoscopeBuilder>();
            Mock.Get(chrono)
                .Setup(x => x.ConfigureServices(It.IsAny<Action<HostBuilderContext, IServiceCollection>>()))
                .Callback((Action<HostBuilderContext, IServiceCollection> action) => builder.ConfigureServices(action));
            var name = Guid.NewGuid().ToString();

            // act
            chrono.AddLoggerSink(options =>
            {
                options.ScopeCreatedEventOptions.EventName = name;
            });

            // assert
            using var host = builder.Build();

            Assert.IsType<LoggerSink>(host.Services.GetRequiredService<ITrackingSink>());

            var options = host.Services.GetRequiredService<IOptions<LoggerSinkOptions>>().Value;
            Assert.Equal(name, options.ScopeCreatedEventOptions.EventName);
        }

        [Fact]
        public void AddLoggerSinkWithNoOptionsWorks()
        {
            // arrange
            var builder = new HostBuilder();
            var chrono = Mock.Of<IChronoscopeBuilder>();
            Mock.Get(chrono)
                .Setup(x => x.ConfigureServices(It.IsAny<Action<HostBuilderContext, IServiceCollection>>()))
                .Callback((Action<HostBuilderContext, IServiceCollection> action) => builder.ConfigureServices(action));

            // act
            chrono.AddLoggerSink();

            // assert
            using var host = builder.Build();

            var result1 = host.Services.GetRequiredService<ITrackingSink>();
            Assert.IsType<LoggerSink>(result1);
        }
    }
}