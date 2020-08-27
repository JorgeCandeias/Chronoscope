using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The event raised upon creating a new scope.
    /// </summary>
    public interface IScopeCreatedEvent : ITrackingEvent
    {
        string? Name { get; }

        Guid? ParentScopeId { get; }
    }
}