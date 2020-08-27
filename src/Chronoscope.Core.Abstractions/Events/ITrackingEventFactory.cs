using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Factory that creates scope created events.
    /// </summary>
    public interface ITrackingEventFactory
    {
        IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp);

        ITrackerCreatedEvent CreateTrackerCreatedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed);

        ITrackerStartedEvent CreateTrackerStartedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed);

        ITrackerStoppedEvent CreateTrackerStoppedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed);

        ITrackerCompletedEvent CreateTrackerCompletedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed);

        ITrackerFaultedEvent CreateTrackerFaultedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, Exception? exception);
    }
}