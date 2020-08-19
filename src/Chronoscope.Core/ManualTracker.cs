using System;
using System.Diagnostics;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IManualTracker"/>.
    /// </summary>
    internal class ManualTracker : IManualTracker
    {
        public ManualTracker(Guid scopeId)
        {
            ScopeId = scopeId;
        }

        private readonly Stopwatch _watch = new Stopwatch();

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