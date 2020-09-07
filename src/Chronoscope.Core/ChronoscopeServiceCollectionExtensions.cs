using Chronoscope.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Chronoscope
{
    /// <summary>
    /// Quality-of-life dependency injection extensions for <see cref="IChronoscope"/>.
    /// </summary>
    public static class ChronoscopeServiceCollectionExtensions
    {
        public static IServiceCollection AddChronoscope(this IServiceCollection services)
        {
            return services
                .AddSingleton<IChronoscope, Chronoscope>()
                .AddSingleton<ITrackingScopeFactory, TrackingScopeFactory>()
                .AddSingleton<ITrackerFactory, TrackerFactory>()
                .AddSingleton<ITrackingSinks, TrackingSinks>()
                .AddSingleton<ITrackingEventFactory, TrackingEventFactory>()
                .AddSingleton<ITrackingScopeStack, TrackingScopeStack>()
                .AddSingleton<ISystemClock, SystemClock>()
                .AddSingleton<IChronoscopeContext, ChronoscopeContext>()
                .AddOptions<ChronoscopeOptions>().ValidateDataAnnotations().Services;
        }
    }
}