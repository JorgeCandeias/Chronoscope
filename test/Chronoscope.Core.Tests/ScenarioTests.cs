using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class ScenarioTests
    {
        [Fact]
        public void Tracks1()
        {
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();

            using (var host = Host
                .CreateDefaultBuilder()
                .UseChronoscope(chrono =>
                {
                })
                .Build())
            {
                var chrono = host.Services.GetRequiredService<IChronoscope>();
                var scope = chrono.CreateScope(id, name);
                var tracker = scope.CreateTracker();

                tracker.Start();
                Task.Delay(TimeSpan.FromMilliseconds(100)).GetAwaiter().GetResult();
                tracker.Stop();

                Assert.True(tracker.Elapsed >= TimeSpan.FromMilliseconds(100));
                Assert.True(tracker.Elapsed <= TimeSpan.FromMilliseconds(150));
            }
        }
    }
}