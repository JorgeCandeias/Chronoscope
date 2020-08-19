namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerStopwatchFactory"/>.
    /// </summary>
    internal class TrackerStopwatchFactory : ITrackerStopwatchFactory
    {
        public ITrackerStopwatch Create()
        {
            return new TrackerStopwatch();
        }
    }
}