using Chronoscope.Events;
using Chronoscope.Tests.Fakes;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Sinks.Logger.Tests
{
    public class LoggerSinkTests
    {
        [Fact]
        public void ConstructorThrowsOnNullLogger()
        {
            // arrange
            ILogger<LoggerSink> logger = null;
            IOptions<LoggerSinkOptions> options = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new LoggerSink(logger, options));

            // assert
            Assert.Equal(nameof(logger), ex.ParamName);
        }

        [Fact]
        public void ConstructorThrowsOnNullOptions()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            IOptions<LoggerSinkOptions> options = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => new LoggerSink(logger, options));

            // assert
            Assert.Equal(nameof(options), ex.ParamName);
        }

        [Fact]
        public void SinkThrowsOnNullTrackingEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions());
            var sink = new LoggerSink(logger, options);
            ITrackingEvent trackingEvent = null;

            // act
            var ex = Assert.Throws<ArgumentNullException>(() => sink.Sink(trackingEvent));

            // assert
            Assert.Equal(nameof(trackingEvent), ex.ParamName);
        }

        [Fact]
        public void SinkThrowsOnUnknownEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = Mock.Of<ITrackingEvent>();

            // act
            var ex = Assert.Throws<InvalidOperationException>(() => sink.Sink(evt));

            // assert
            Assert.Contains(evt.GetType().Name, ex.Message, StringComparison.Ordinal);
        }

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

        [Fact]
        public void SinksTrackerCreatedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerCreatedEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(2, x.EventId.Id);
                    Assert.Equal("TrackerCreated", x.EventId.Name);
                    Assert.Equal(LogLevel.Information, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }

        [Fact]
        public void SinksTrackerStartedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerStartedEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(3, x.EventId.Id);
                    Assert.Equal("TrackerStarted", x.EventId.Name);
                    Assert.Equal(LogLevel.Information, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }

        [Fact]
        public void SinksTrackerStoppedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerStoppedEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(4, x.EventId.Id);
                    Assert.Equal("TrackerStopped", x.EventId.Name);
                    Assert.Equal(LogLevel.Information, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }

        [Fact]
        public void SinksTrackerCompletedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerCompletedEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(5, x.EventId.Id);
                    Assert.Equal("TrackerCompleted", x.EventId.Name);
                    Assert.Equal(LogLevel.Information, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }

        [Fact]
        public void SinksTrackerFaultedEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerFaultedEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(6, x.EventId.Id);
                    Assert.Equal("TrackerFaulted", x.EventId.Name);
                    Assert.Equal(LogLevel.Error, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }

        [Fact]
        public void SinksTrackerCancelledEvent()
        {
            // arrange
            var logger = new FakeLogger<LoggerSink>();
            var options = Options.Create(new LoggerSinkOptions
            {
            });
            var sink = new LoggerSink(logger, options);
            var evt = new FakeTrackerCancelledEvent
            {
                ScopeId = Guid.NewGuid(),
                Timestamp = DateTimeOffset.Now,
                TrackerId = Guid.NewGuid(),
                Elapsed = TimeSpan.FromSeconds(123)
            };

            // act
            sink.Sink(evt);

            // assert
            Assert.Collection(logger.Items,
                x =>
                {
                    Assert.Equal(7, x.EventId.Id);
                    Assert.Equal("TrackerCancelled", x.EventId.Name);
                    Assert.Equal(LogLevel.Error, x.LogLevel);
                    Assert.Null(x.Exception);
                });
        }
    }
}