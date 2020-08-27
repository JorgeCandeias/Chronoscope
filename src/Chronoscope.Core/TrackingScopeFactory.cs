using Chronoscope.Events;
using Microsoft.Extensions.Options;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingScopeFactory"/>.
    /// </summary>
    internal class TrackingScopeFactory : ITrackingScopeFactory
    {
        private readonly IOptions<ChronoscopeOptions> _options;
        private readonly ITrackerFactory _trackerFactory;
        private readonly ITrackingSinks _sinks;
        private readonly ITrackingEventFactory _trackingEventFactory;
        private readonly ISystemClock _clock;

        public TrackingScopeFactory(IOptions<ChronoscopeOptions> options, ITrackerFactory trackerFactory, ITrackingSinks sinks, ITrackingEventFactory trackingEventFactory, ISystemClock clock)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _trackerFactory = trackerFactory ?? throw new ArgumentNullException(nameof(trackerFactory));
            _sinks = sinks ?? throw new ArgumentNullException(nameof(sinks));
            _trackingEventFactory = trackingEventFactory ?? throw new ArgumentNullException(nameof(trackingEventFactory));
            _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public ITrackingScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            return new TrackingScope(_options, this, _trackerFactory, _sinks, _trackingEventFactory, _clock, id, name, parentId);
        }
    }
}