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

        public ITrackerCreatedEvent CreateTrackerCreatedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new TrackerCreatedEvent(scopeId, trackerId, timestamp, elapsed);
        }

        public ITrackerStartedEvent CreateTrackerStartedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new TrackerStartedEvent(scopeId, trackerId, timestamp, elapsed);
        }

        public ITrackerStoppedEvent CreateTrackerStoppedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new TrackerStoppedEvent(scopeId, trackerId, timestamp, elapsed);
        }
    }
}