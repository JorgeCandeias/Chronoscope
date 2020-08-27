using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerFactory"/>.
    /// </summary>
    internal class TrackerFactory : ITrackerFactory
    {
        private readonly ITrackerStopwatchFactory _stopWatchFactory;

        public TrackerFactory(ITrackerStopwatchFactory stopWatchFactory)
        {
            _stopWatchFactory = stopWatchFactory;
        }

        public IManualTracker CreateTracker(Guid id, Guid scopeId)
        {
            var watch = _stopWatchFactory.Create();

            return new ManualTracker(watch, id, scopeId);
        }
    }
}