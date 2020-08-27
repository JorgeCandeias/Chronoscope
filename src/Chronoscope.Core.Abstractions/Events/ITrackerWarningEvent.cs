using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The event raised when a tracker issues a warning, such as when an activity takes longer that its threshold.
    /// </summary>
    public interface ITrackerWarningEvent : ITrackerEvent
    {
        /// <summary>
        /// The exception that caused this warning.
        /// </summary>
        Exception? Exception { get; }

        /// <summary>
        /// A message relevant to this warning.
        /// </summary>
        string? Message { get; }
    }
}