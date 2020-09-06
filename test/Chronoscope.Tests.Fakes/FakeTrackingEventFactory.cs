using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackingEventFactory : ITrackingEventFactory
    {
        public IScopeCreatedEvent CreateScopeCreatedEvent(Guid scopeId, string name, Guid? parentScopeId, DateTimeOffset timestamp)
        {
            return new FakeScopeCreatedEvent
            {
                ScopeId = scopeId,
                Name = name,
                ParentScopeId = parentScopeId,
                Timestamp = timestamp
            };
        }

        public ITrackerCancelledEvent CreateTrackerCancelledEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, Exception exception)
        {
            return new FakeTrackerCancelledEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed,
                Exception = exception
            };
        }

        public ITrackerCompletedEvent CreateTrackerCompletedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new FakeTrackerCompletedEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed
            };
        }

        public ITrackerCreatedEvent CreateTrackerCreatedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new FakeTrackerCreatedEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed
            };
        }

        public ITrackerFaultedEvent CreateTrackerFaultedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed, Exception exception)
        {
            return new FakeTrackerFaultedEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed,
                Exception = exception
            };
        }

        public ITrackerStartedEvent CreateTrackerStartedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new FakeTrackerStartedEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed
            };
        }

        public ITrackerStoppedEvent CreateTrackerStoppedEvent(Guid scopeId, Guid trackerId, DateTimeOffset timestamp, TimeSpan elapsed)
        {
            return new FakeTrackerStoppedEvent
            {
                ScopeId = scopeId,
                TrackerId = trackerId,
                Timestamp = timestamp,
                Elapsed = elapsed
            };
        }
    }
}