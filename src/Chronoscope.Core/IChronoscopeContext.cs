using Microsoft.Extensions.Options;

namespace Chronoscope
{
    /// <summary>
    /// Defines a set of shared services across chronoscope related instances as to minimize individual memory footprint.
    /// </summary>
    internal interface IChronoscopeContext
    {
        /// <summary>
        /// Gets the configured <see cref="IOptions{ChronoscopeOptions}"/> instance.
        /// </summary>
        IOptions<ChronoscopeOptions> Options { get; }

        /// <summary>
        /// Gets the configured <see cref="ITrackingScopeFactory"/> service.
        /// </summary>
        ITrackingScopeFactory TrackingScopeFactory { get; }

        /// <summary>
        /// Gets the configured <see cref="ITrackerFactory"/> service.
        /// </summary>
        ITrackerFactory TrackerFactory { get; }
    }
}