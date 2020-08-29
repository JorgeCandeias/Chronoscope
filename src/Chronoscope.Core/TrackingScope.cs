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

        internal TrackingScope(IOptions<ChronoscopeOptions> options, ITrackingScopeFactory factory, ITrackerFactory trackerFactory, ITrackingSinks sinks, ITrackingEventFactory trackingEventFactory, ISystemClock clock, Guid id, string? name, Guid? parentId)
        {
            if (sinks is null) throw new ArgumentNullException(nameof(sinks));
            if (trackingEventFactory is null) throw new ArgumentNullException(nameof(trackingEventFactory));
            if (clock is null) throw new ArgumentNullException(nameof(clock));

            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;

            sinks.Sink(trackingEventFactory.CreateScopeCreatedEvent(Id, Name, ParentId, clock.Now));
        }

        public Guid Id { get; }

        public string? Name { get; }

        public Guid? ParentId { get; }

        public ITrackingScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, Id);

        public IManualTracker CreateManualTracker(Guid id) => _trackerFactory.CreateManualTracker(id, Id);

        public IAutoTracker CreateAutoTracker(Guid id) => _trackerFactory.CreateAutoSyncTracker(id, this);
    }
}