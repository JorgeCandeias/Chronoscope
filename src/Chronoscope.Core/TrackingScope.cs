using Chronoscope.Events;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingScope"/>.
    /// </summary>
    internal class TrackingScope : ITrackingScope
    {
        private readonly ITrackingScopeFactory _factory;
        private readonly ITrackerFactory _trackerFactory;
        private readonly ITrackingSinks _sinks;
        private readonly ITrackingEventFactory _trackingEventFactory;
        private readonly ISystemClock _clock;

        internal TrackingScope(IOptions<ChronoscopeOptions> options, ITrackingScopeFactory factory, ITrackerFactory trackerFactory, ITrackingSinks sinks, ITrackingEventFactory trackingEventFactory, ISystemClock clock, Guid id, string? name, Guid? parentId)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
            _sinks = sinks ?? throw new ArgumentNullException(nameof(sinks));
            _trackingEventFactory = trackingEventFactory ?? throw new ArgumentNullException(nameof(trackingEventFactory));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;

            _sinks.Sink(_trackingEventFactory.CreateScopeCreatedEvent(Id, Name, ParentId, _clock.Now));
        }

        public Guid Id { get; }

        public string? Name { get; }

        public Guid? ParentId { get; }

        public ITrackingScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, Id);

        public IManualTracker CreateManualTracker(Guid id) => _trackerFactory.CreateManualTracker(id, Id);

        public IAutoSyncTracker CreateAutoSyncTracker(Guid id) => _trackerFactory.CreateAutoSyncTracker(id, this);
    }
}