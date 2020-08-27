using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The event generated when a new tracker is created.
    /// </summary>
    public interface ITrackerCreatedEvent : ITrackingEvent
    {
        Guid TrackerId { get; }
    }
}