using Chronoscope.Events;
using System;

namespace Chronoscope
{
    internal class ChronoscopeContext : IChronoscopeContext
    {
        public ChronoscopeContext(ISystemClock clock, ITrackingSinks sink, ITrackingEventFactory eventFactory, ITrackerStopwatchFactory stopwatchFactory, ITrackingScopeFactory scopeFactory, ITrackerFactory trackerFactory, ITrackingScopeStack trackingScopeStack)
        {
            Clock = clock ?? throw new ArgumentNullException(nameof(clock));
            Sink = sink ?? throw new ArgumentNullException(nameof(sink));
            EventFactory = eventFactory ?? throw new ArgumentNullException(nameof(eventFactory));
            StopwatchFactory = stopwatchFactory ?? throw new ArgumentNullException(nameof(stopwatchFactory));
            ScopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
            TrackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
            TrackingScopeStack = trackingScopeStack ?? throw new ArgumentNullException(nameof(trackingScopeStack));
        }

        public ISystemClock Clock { get; }
        public ITrackingSinks Sink { get; }
        public ITrackingEventFactory EventFactory { get; }
        public ITrackerStopwatchFactory StopwatchFactory { get; }
        public ITrackingScopeFactory ScopeFactory { get; }
        public ITrackerFactory TrackerFactory { get; }
        public ITrackingScopeStack TrackingScopeStack { get; }
    }
}