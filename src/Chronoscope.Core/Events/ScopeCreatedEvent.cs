using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Default implementation of <see cref="IScopeCreatedEvent"/>.
    /// </summary>
    internal class ScopeCreatedEvent : IScopeCreatedEvent
    {
        public ScopeCreatedEvent(Guid scopeId, string? name, Guid? parentScopeId, DateTimeOffset timestamp)
        {
            ScopeId = scopeId;
            Name = name;
            ParentScopeId = parentScopeId;
            Timestamp = timestamp;
        }

        public Guid ScopeId { get; }

        public string? Name { get; }

        public Guid? ParentScopeId { get; }

        public DateTimeOffset Timestamp { get; }
    }
}