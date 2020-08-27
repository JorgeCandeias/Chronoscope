using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingEventFactory"/>.
    /// </summary>
    internal class TrackingEventFactory : ITrackingEventFactory
    {
        public IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp)
        {
            return new ScopeCreatedEvent(scopeId, name, parentScopeId, timestamp);
        }

        public ITrackerCreatedEvent CreateTrackerCreatedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp)
        {
            return new TrackerCreatedEvent(scopeId, trackingId, timestamp);
        }

        public ITrackerStartedEvent CreateTrackerStartedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp)
        {
            return new TrackerStartedEvent(scopeId, trackingId, timestamp);
        }
    }
}