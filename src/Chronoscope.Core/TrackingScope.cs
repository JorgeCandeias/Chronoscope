using System;
using System.Globalization;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingScope"/>.
    /// </summary>
    internal class TrackingScope : ITrackingScope
    {
        private readonly IChronoscopeContext _context;

        internal TrackingScope(IChronoscopeContext context, Guid id, string? name, Guid? parentId)
        {
            _context = context;

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, context.Options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;
        }

        public Guid Id { get; }

        public string Name { get; }

        public Guid? ParentId { get; }

        public ITrackingScope CreateScope(Guid id, string? name) => _context.TrackingScopeFactory.CreateScope(id, name, Id);

        public IManualTracker CreateTracker() => _context.TrackerFactory.CreateTracker(Id);
    }
}