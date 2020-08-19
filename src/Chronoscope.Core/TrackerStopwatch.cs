using System;
using System.Diagnostics;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerStopwatch"/>.
    /// </summary>
    public class TrackerStopwatch : ITrackerStopwatch
    {
        private readonly Stopwatch _watch = new Stopwatch();

        public TimeSpan Elapsed => _watch.Elapsed;

        public void Start() => _watch.Start();

        public void Stop() => _watch.Stop();
    }
}