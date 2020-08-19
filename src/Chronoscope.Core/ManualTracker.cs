using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IManualTracker"/>.
    /// </summary>
    internal class ManualTracker : IManualTracker
    {
        private readonly ITrackerStopwatch _watch;

        public ManualTracker(ITrackerStopwatch watch, Guid scopeId)
        {
            _watch = watch;
            ScopeId = scopeId;
        }

        public Guid Id { get; } = Guid.NewGuid();
        public Guid ScopeId { get; }

        public TimeSpan Elapsed => _watch.Elapsed;

        public void Start()
        {
            _watch.Start();
        }

        public void Stop()
        {
            _watch.Stop();
        }
    }
}