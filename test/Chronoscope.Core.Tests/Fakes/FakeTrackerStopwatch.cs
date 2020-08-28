using System;

namespace Chronoscope.Core.Tests.Fakes
{
    public class FakeTrackerStopwatch : ITrackerStopwatch
    {
        public TimeSpan Elapsed { get; set; }

        public TimeSpan TargetElapsed { get; set; }

        public bool IsRunning { get; private set; }

        public void Start()
        {
            IsRunning = true;
        }

        public void Stop()
        {
            IsRunning = false;
            Elapsed = TargetElapsed;
        }
    }
}