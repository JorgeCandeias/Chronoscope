using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracker that allows the user to start and stop tracking manually and does not directly monitor task execution.
    /// Appropriate for when the start of a task and its completion are dissociated from each other in code.
    /// </summary>
    public interface IManualTracker : ITracker
    {
        void Start();

        void Stop();

        void Complete();

        TimeSpan Elapsed { get; }
    }
}