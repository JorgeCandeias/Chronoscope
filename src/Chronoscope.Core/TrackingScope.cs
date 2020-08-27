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

        internal TrackingScope(IOptions<ChronoscopeOptions> options, ITrackingScopeFactory factory, ITrackerFactory trackerFactory, Guid id, string? name, Guid? parentId)
        {
            _factory = factory;
            _trackerFactory = trackerFactory;

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;
        }

        public Guid Id { get; }

        public string Name { get; }

        public Guid? ParentId { get; }

        public ITrackingScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, Id);

        public IManualTracker CreateManualTracker(Guid id) => _trackerFactory.CreateTracker(id, Id);
    }
}