using Chronoscope.Properties;
using System;
using System.Threading;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IManualTracker"/>.
    /// </summary>
    internal class ManualTracker : IManualTracker
    {
        private readonly IChronoscopeContext _context;
        private readonly ITrackerStopwatch _watch;

        public ManualTracker(IChronoscopeContext context, Guid id, Guid scopeId)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _watch = _context.StopwatchFactory.Create();

            Id = id;
            ScopeId = scopeId;

            _context.Sink.Sink(_context.EventFactory.CreateTrackerCreatedEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed));
        }

        public Guid Id { get; }
        public Guid ScopeId { get; }

        public TimeSpan Elapsed => _watch.Elapsed;

        private int _tracking;

        private void AllowTrackingOnce()
        {
            if (Interlocked.CompareExchange(ref _tracking, 1, 0) == 0)
            {
                return;
            }

            throw new InvalidOperationException(Resources.Exception_ThisTrackerIsAlreadyTrackingAnotherAction);
        }

        public void Start()
        {
            AllowTrackingOnce();

            _context.Sink.Sink(_context.EventFactory.CreateTrackerStartedEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed));
            _watch.Start();
        }

        public void Stop()
        {
            _watch.Stop();

            _context.Sink.Sink(_context.EventFactory.CreateTrackerStoppedEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed));
        }

        private void EnsureStopped()
        {
            if (_watch.IsRunning)
            {
                Stop();
            }
        }

        public void Complete()
        {
            EnsureStopped();

            _context.Sink.Sink(_context.EventFactory.CreateTrackerCompletedEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed));
        }

        public void Fault(Exception? exception)
        {
            EnsureStopped();

            _context.Sink.Sink(_context.EventFactory.CreateTrackerFaultedEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed, exception));
        }

        public void Cancel(Exception? exception)
        {
            EnsureStopped();

            _context.Sink.Sink(_context.EventFactory.CreateTrackerCancelledEvent(ScopeId, Id, _context.Clock.Now, _watch.Elapsed, exception));
        }
    }
}