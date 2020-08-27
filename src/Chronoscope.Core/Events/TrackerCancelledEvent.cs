using System;

namespace Chronoscope.Events
{
    public class TrackerCancelledEvent : ITrackerCancelledEvent
    {
        public TrackerCancelledEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, Exception? exception)
        {
            ScopeId = scopeId;
            TrackerId = trackerId;
            Timestamp = timestamp;
            Elapsed = elapsed;
            Exception = exception;
        }

        public Guid ScopeId { get; }

        public Guid TrackerId { get; }

        public DateTimeOffset Timestamp { get; }

        public TimeSpan Elapsed { get; }

        public Exception? Exception { get; }
    }
}