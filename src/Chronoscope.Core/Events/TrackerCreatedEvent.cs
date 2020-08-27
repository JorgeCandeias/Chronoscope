using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerCreatedEvent"/>.
    /// </summary>
    internal class TrackerCreatedEvent : ITrackerCreatedEvent
    {
        public TrackerCreatedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp)
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