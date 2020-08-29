using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeScopeCreatedEvent : IScopeCreatedEvent
    {
        public string Name { get; set; }

        public Guid? ParentScopeId { get; set; }

        public Guid ScopeId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}