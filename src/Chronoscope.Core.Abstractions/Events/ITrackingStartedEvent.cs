using System;

namespace Chronoscope.Events
{
    public interface ITrackingStartedEvent : ITrackingEvent
    {
        Guid TrackingId { get; }
    }
}