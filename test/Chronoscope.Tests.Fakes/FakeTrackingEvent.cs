using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackingEvent : ITrackingEvent
    {
        public Guid ScopeId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}