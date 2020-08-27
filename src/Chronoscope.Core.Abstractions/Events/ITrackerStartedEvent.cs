using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The event raised when a tracker starts tracking.
    /// </summary>
    public interface ITrackerStartedEvent : ITrackingEvent
    {
        Guid TrackingId { get; }
    }
}