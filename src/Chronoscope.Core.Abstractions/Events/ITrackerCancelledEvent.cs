using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The event raised when a tracker is cancelled.
    /// </summary>
    public interface ITrackerCancelledEvent : ITrackerEvent
    {
        Exception? Exception { get; }
    }
}