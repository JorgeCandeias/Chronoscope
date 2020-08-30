using Chronoscope.Events;

namespace Chronoscope.Tests.Fakes
{
    public class FakeChronoscopeContext : IChronoscopeContext
    {
        public ISystemClock Clock { get; set; }

        public ITrackingSinks Sink { get; set; }

        public ITrackingEventFactory EventFactory { get; set; }

        public ITrackerStopwatchFactory StopwatchFactory { get; set; }

        public ITrackingScopeFactory ScopeFactory { get; set; }

        public ITrackerFactory TrackerFactory { get; set; }
    }
}