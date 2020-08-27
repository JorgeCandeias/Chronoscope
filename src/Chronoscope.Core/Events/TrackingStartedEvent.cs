using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingStartedEvent"/>.
    /// </summary>
    internal class TrackingStartedEvent : ITrackingStartedEvent
    {
        public TrackingStartedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp)
        {
            ScopeId = scopeId;
            TrackingId = trackingId;
            Timestamp = timestamp;
        }

        public Guid ScopeId { get; }

        public Guid TrackingId { get; }

        public DateTimeOffset Timestamp { get; }
    }
}