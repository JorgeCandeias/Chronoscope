using Chronoscope.Events;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IManualTracker"/>.
    /// </summary>
    internal class ManualTracker : IManualTracker
    {
        private readonly ITrackerStopwatch _watch;
        private readonly ITrackingEventFactory _trackingEventFactory;
        private readonly ITrackingSinks _sink;
        private readonly ISystemClock _clock;

        public ManualTracker(ITrackerStopwatch watch, ITrackingEventFactory trackingEventFactory, ITrackingSinks sink, ISystemClock clock, Guid id, Guid scopeId)
        {
            _watch = watch ?? throw new ArgumentNullException(nameof(watch));
            _trackingEventFactory = trackingEventFactory ?? throw new ArgumentNullException(nameof(trackingEventFactory));
            _sink = sink ?? throw new ArgumentNullException(nameof(sink));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));

            Id = id;
            ScopeId = scopeId;

            _sink.Sink(_trackingEventFactory.CreateTrackerCreatedEvent(ScopeId, Id, _clock.Now, _watch.Elapsed));
        }

        public Guid Id { get; }
        public Guid ScopeId { get; }

        public TimeSpan Elapsed => _watch.Elapsed;

        public void Start()
        {
            _sink.Sink(_trackingEventFactory.CreateTrackerStartedEvent(ScopeId, Id, _clock.Now, _watch.Elapsed));

            _watch.Start();
        }

        public void Stop()
        {
            _watch.Stop();

            _sink.Sink(_trackingEventFactory.CreateTrackerStoppedEvent(ScopeId, Id, _clock.Now, _watch.Elapsed));
        }

        public void Complete()
        {
            if (_watch.IsRunning)
            {
                Stop();
            }

            _sink.Sink(_trackingEventFactory.CreateTrackerCompletedEvent(ScopeId, Id, _clock.Now, _watch.Elapsed));
        }
    }
}