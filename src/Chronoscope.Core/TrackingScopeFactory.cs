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

        public TrackingScopeFactory(IOptions<ChronoscopeOptions> options, ITrackerFactory trackerFactory)
        {
            _options = options;
            _trackerFactory = trackerFactory;
        }

        public ITrackingScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            return new TrackingScope(_options, this, _trackerFactory, id, name, parentId);
        }
    }
}