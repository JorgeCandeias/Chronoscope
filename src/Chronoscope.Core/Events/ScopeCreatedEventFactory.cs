using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingEventFactory"/>.
    /// </summary>
    internal class ScopeCreatedEventFactory : ITrackingEventFactory
    {
        public IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp)
        {
            return new ScopeCreatedEvent(scopeId, name, parentScopeId, timestamp);
        }
    }
}