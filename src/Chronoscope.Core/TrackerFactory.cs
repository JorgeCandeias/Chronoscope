using Chronoscope.Events;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerFactory"/>.
    /// </summary>
    internal class TrackerFactory : ITrackerFactory
    {
        private readonly ITrackerStopwatchFactory _stopWatchFactory;
        private readonly ITrackingEventFactory _trackingEventFactory;
        private readonly ITrackingSinks _trackingSinks;
        private readonly ISystemClock _clock;

        public TrackerFactory(ITrackerStopwatchFactory stopWatchFactory, ITrackingEventFactory trackingEventFactory, ITrackingSinks trackingSinks, ISystemClock clock)
        {
            _stopWatchFactory = stopWatchFactory ?? throw new ArgumentNullException(nameof(stopWatchFactory));
            _trackingEventFactory = trackingEventFactory ?? throw new ArgumentNullException(nameof(trackingEventFactory));
            _trackingSinks = trackingSinks ?? throw new ArgumentNullException(nameof(trackingSinks));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public IAutoSyncTracker CreateAutoSyncTracker(Guid id, ITrackingScope scope)
        {
            var watch = _stopWatchFactory.Create();

            return new AutoSyncTracker(watch, _trackingEventFactory, _trackingSinks, _clock, id, scope);
        }

        public IManualTracker CreateManualTracker(Guid id, Guid scopeId)
        {
            var watch = _stopWatchFactory.Create();

            return new ManualTracker(watch, _trackingEventFactory, _trackingSinks, _clock, id, scopeId);
        }
    }
}