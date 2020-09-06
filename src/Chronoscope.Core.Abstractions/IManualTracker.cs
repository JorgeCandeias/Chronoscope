using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracker that allows the user to start and stop tracking manually and does not directly monitor task execution.
    /// Appropriate for when the start of a task and its completion are dissociated from each other in code.
    /// </summary>
    public interface IManualTracker : ITracker
    {
        /// <summary>
        /// Starts measuring time.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops measuring time.
        /// </summary>
        void Stop();

        /// <summary>
        /// Sets the tracking activity as completed.
        /// </summary>
        void Complete();

        /// <summary>
        /// Sets the tracking activity as faulted.
        /// </summary>
        /// <param name="exception">The exception that caused the fault.</param>
        void Fault(Exception? exception);

        /// <summary>
        /// Sets the tracking activity as cancelled.
        /// </summary>
        /// <param name="exception">The exception that caused the cancellation.</param>
        void Cancel(Exception? exception);
    }
}