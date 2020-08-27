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
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            // arrange a stopwatch that tracks a known interval
            var elapsed = TimeSpan.FromSeconds(123);
            var watch = Mock.Of<ITrackerStopwatch>();
            Mock.Get(watch).Setup(x => x.Stop()).Callback(() => Mock.Get(watch).Setup(x => x.Elapsed).Returns(elapsed));
            var factory = Mock.Of<ITrackerStopwatchFactory>(x => x.Create() == watch);

            // act - build host
            using (var host = Host
                .CreateDefaultBuilder()
                .UseChronoscope(chrono =>
                {
                    chrono.ConfigureServices(services =>
                    {
                        services.AddSingleton(factory);
                    });
                })
                .Build())
            {
                // act - request services
                var chrono = host.Services.GetRequiredService<IChronoscope>();
                var scope = chrono.CreateScope(id, name);
                var tracker = scope.CreateManualTracker();

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
            }
        }
    }
}