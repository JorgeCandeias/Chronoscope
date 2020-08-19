using Microsoft.Extensions.Options;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IChronoscopeContext"/>.
    /// </summary>
    internal class ChronoscopeContext : IChronoscopeContext
    {
        public ChronoscopeContext(IOptions<ChronoscopeOptions> options, ITrackingScopeFactory trackingScopeFactory, ITrackerFactory trackerFactory)
        {
            Options = options;
            TrackingScopeFactory = trackingScopeFactory;
            TrackerFactory = trackerFactory;
        }

        public IOptions<ChronoscopeOptions> Options { get; }

        public ITrackingScopeFactory TrackingScopeFactory { get; }

        public ITrackerFactory TrackerFactory { get; }
    }
}