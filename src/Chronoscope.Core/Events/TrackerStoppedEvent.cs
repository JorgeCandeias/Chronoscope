using System;

namespace Chronoscope.Events
{
    internal class TrackerStoppedEvent : ITrackerStoppedEvent
    {
        public TrackerStoppedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            ScopeId = scopeId;
            TrackerId = trackerId;
            Timestamp = timestamp;
            Elapsed = elapsed;
        }

        public Guid ScopeId { get; }

        public Guid TrackerId { get; }

        public DateTimeOffset Timestamp { get; }

        public TimeSpan Elapsed { get; }
    }
}