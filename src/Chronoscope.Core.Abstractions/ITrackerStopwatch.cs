using System;

namespace Chronoscope
{
    /// <summary>
    /// Abstracts the <see cref="System.Diagnostics.Stopwatch"/> class to ease unit testing.
    /// </summary>
    public interface ITrackerStopwatch
    {
        void Start();

        void Stop();

        TimeSpan Elapsed { get; }
    }
}