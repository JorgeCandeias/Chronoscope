using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IManualTracker"/>.
    /// </summary>
    internal class ManualTracker : IManualTracker
    {
        private readonly ITrackerStopwatch _watch;

        public ManualTracker(ITrackerStopwatch watch, Guid id, Guid scopeId)
        {
            _watch = watch;
            Id = id;
            ScopeId = scopeId;
        }

        public Guid Id { get; }
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