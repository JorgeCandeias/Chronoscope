using System;

namespace Chronoscope
{
    internal class TrackerFactory : ITrackerFactory
    {
        public IManualTracker CreateTracker(Guid scopeId)
        {
            return new ManualTracker(scopeId);
        }
    }
}