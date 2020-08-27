using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// Event raised when a tracker faults.
    /// </summary>
    public interface ITrackerFaultedEvent : ITrackerEvent
    {
        /// <summary>
        /// The exception that caused the tracker to fault.
        /// </summary>
        Exception? Exception { get; }
    }
}