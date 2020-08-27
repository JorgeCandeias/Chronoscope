using System;

namespace Chronoscope.Events
{
    internal class TrackerWarningEvent : ITrackerWarningEvent
    {
        public TrackerWarningEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, string? message, Exception? exception)
        {
            ScopeId = scopeId;
            TrackerId = trackerId;
            Timestamp = timestamp;
            Elapsed = elapsed;
            Message = message;
            Exception = exception;
        }

        public Guid ScopeId { get; }

        public Guid TrackerId { get; }

        public DateTimeOffset Timestamp { get; }

        public TimeSpan Elapsed { get; }

        public string? Message { get; }

        public Exception? Exception { get; }
    }
}