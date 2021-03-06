﻿using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerStartedEvent"/>.
    /// </summary>
    internal class TrackerStartedEvent : ITrackerStartedEvent
    {
        public TrackerStartedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
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