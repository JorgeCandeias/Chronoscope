using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Factory that creates scope created events.
    /// </summary>
    public interface ITrackingEventFactory
    {
        IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp);

        ITrackingStartedEvent CreateTrackingStartedEvent(Guid scopeId, Guid trackingId, DateTimeOffset timestamp);
    }
}