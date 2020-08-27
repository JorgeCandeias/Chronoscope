using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerStartedEvent"/>.
    /// </summary>
    internal class TrackerStartedEvent : ITrackerStartedEvent
    {
        public TrackerStartedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp)
        {
            ScopeId = scopeId;
            TrackerId = trackingId;
            Timestamp = timestamp;
        }

        public Guid ScopeId { get; }

        public Guid TrackerId { get; }

        public DateTimeOffset Timestamp { get; }
    }
}