using Chronoscope.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using Xunit;

namespace Chronoscope.Sinks.Logger.Tests
{
    public class LoggerSinkTests
    {
        [Fact]
        public void SinksScopeCreatedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeScopeCreatedEvent
            {
                ScopeId = Guid.NewGuid(),
                Name = Guid.NewGuid().ToString(),
                ParentScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(1, x.EventId.Id);
                    Assert.Equal("ScopeCreated", x.EventId.Name);
                    Assert.Equal(LogLevel.Information, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }
    }
}