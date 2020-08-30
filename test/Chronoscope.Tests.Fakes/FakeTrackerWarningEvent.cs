using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackerWarningEvent : ITrackerWarningEvent
    {
        public Exception Exception { get; set; }

        public string Message { get; set; }

        public Guid TrackerId { get; set; }

        public TimeSpan Elapsed { get; set; }

        public Guid ScopeId { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}