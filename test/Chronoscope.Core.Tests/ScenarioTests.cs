using Chronoscope.Core.Tests.Fakes;
using Chronoscope.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ScenarioTests
    {
        [Fact]
        public void SimpleManualTracking()
        {
            // arrange identifiers
            var scopeId = Guid.NewGuid();
            var scopeName = Guid.NewGuid().ToString();
            var trackingId = Guid.NewGuid();

            // arrange a stopwatch that tracks a known interval
            var elapsed = TimeSpan.FromSeconds(123);
            var watch = Mock.Of<ITrackerStopwatch>();
            Mock.Get(watch).Setup(x => x.Stop()).Callback(() => Mock.Get(watch).Setup(x => x.Elapsed).Returns(elapsed));
            var factory = Mock.Of<ITrackerStopwatchFactory>(x => x.Create() == watch);

            // arrange a test sink
            var sink = new FakeSink();

            // arrange a system clock
            var now = DateTimeOffset.Now;
            var clock = Mock.Of<ISystemClock>(x => x.Now == now);

            // act - build host
            using (var host = Host
                .CreateDefaultBuilder()
                .UseChronoscope(chrono =>
                {
                    chrono.ConfigureServices(services =>
                    {
                        services.AddSingleton(factory);
                        services.AddSingleton<ITrackingSink>(sink);
                        services.AddSingleton(clock);
                    });
                })
                .Build())
            {
                // act - request services
                var chrono = host.Services.GetRequiredService<IChronoscope>();
                var scope = chrono.CreateScope(scopeId, scopeName);
                var tracker = scope.CreateManualTracker(trackingId);

                // assert - elapsed is zero
                Assert.Equal(TimeSpan.Zero, tracker.Elapsed);

                // act - start manual tracking
                tracker.Start();

                // assert - stopwatch was started
                Mock.Get(watch).Verify(x => x.Start());
                Assert.Equal(TimeSpan.Zero, tracker.Elapsed);

                // act - stop manual tracking
                tracker.Stop();

                // assert - elapsed time is correct
                Mock.Get(watch).Verify(x => x.Stop());
                Assert.Equal(elapsed, tracker.Elapsed);

                // assert - events were generated
                Assert.Collection(sink.Events,
                    e =>
                    {
                        var x = Assert.IsAssignableFrom<IScopeCreatedEvent>(e);
                        Assert.Equal(scopeId, x.ScopeId);
                        Assert.Equal(scopeName, x.Name);
                        Assert.Null(x.ParentScopeId);
                        Assert.Equal(now, x.Timestamp);
                    },
                    e =>
                    {
                        var x = Assert.IsAssignableFrom<ITrackerCreatedEvent>(e);
                        Assert.Equal(scopeId, x.ScopeId);
                        Assert.Equal(trackingId, x.TrackerId);
                        Assert.Equal(now, x.Timestamp);
                        Assert.Equal(TimeSpan.Zero, x.Elapsed);
                    },
                    e =>
                    {
                        var x = Assert.IsAssignableFrom<ITrackerStartedEvent>(e);
                        Assert.Equal(scopeId, x.ScopeId);
                        Assert.Equal(trackingId, x.TrackerId);
                        Assert.Equal(now, x.Timestamp);
                        Assert.Equal(TimeSpan.Zero, x.Elapsed);
                    },
                    e =>
                    {
                        var x = Assert.IsAssignableFrom<ITrackerStoppedEvent>(e);
                        Assert.Equal(scopeId, x.ScopeId);
                        Assert.Equal(trackingId, x.TrackerId);
                        Assert.Equal(now, x.Timestamp);
                        Assert.Equal(elapsed, x.Elapsed);
                    });
            }
        }
    }
}