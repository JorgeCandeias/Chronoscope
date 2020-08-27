using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingEventFactory"/>.
    /// </summary>
    internal class TrackingEventFactory : ITrackingEventFactory
    {
        public IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp) => new ScopeCreatedEvent(scopeId, name, parentScopeId, timestamp);

        public ITrackerCompletedEvent CreateTrackerCompletedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed) => new TrackerCompletedEvent(scopeId, trackerId, timestamp, elapsed);

        public ITrackerCreatedEvent CreateTrackerCreatedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed) => new TrackerCreatedEvent(scopeId, trackerId, timestamp, elapsed);

        public ITrackerFaultedEvent CreateTrackerFaultedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, Exception? exception) => new TrackerFaultedEvent(scopeId, trackerId, timestamp, elapsed, exception);

        public ITrackerStartedEvent CreateTrackerStartedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed) => new TrackerStartedEvent(scopeId, trackerId, timestamp, elapsed);

        public ITrackerStoppedEvent CreateTrackerStoppedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed) => new TrackerStoppedEvent(scopeId, trackerId, timestamp, elapsed);
    }
}