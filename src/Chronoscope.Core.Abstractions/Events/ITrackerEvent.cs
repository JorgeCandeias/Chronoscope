using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Base interface for events raised by trackers.
    /// </summary>
    public interface ITrackerEvent : ITrackingEvent
    {
        Guid TrackerId { get; }

        TimeSpan Elapsed { get; }
    }
}