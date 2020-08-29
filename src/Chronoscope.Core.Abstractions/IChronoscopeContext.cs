using Chronoscope.Events;

namespace Chronoscope
{
    /// <summary>
    /// A common set of services used across chronoscope, grouped to reduce the memory footprint of redundant references.
    /// </summary>
    public interface IChronoscopeContext
    {
        ISystemClock Clock { get; }
        ITrackingSinks Sink { get; }
        ITrackingEventFactory EventFactory { get; }
        ITrackerStopwatchFactory StopwatchFactory { get; }
        ITrackingScopeFactory ScopeFactory { get; }
        ITrackerFactory TrackerFactory { get; }
    }
}