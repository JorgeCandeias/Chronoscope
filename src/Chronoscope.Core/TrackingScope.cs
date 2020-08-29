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
        private readonly IChronoscopeContext _context;

        internal TrackingScope(IOptions<ChronoscopeOptions> options, IChronoscopeContext context, Guid id, string? name, Guid? parentId)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;

            _context.Sink.Sink(_context.EventFactory.CreateScopeCreatedEvent(Id, Name, ParentId, _context.Clock.Now));
        }

        public Guid Id { get; }

        public string? Name { get; }

        public Guid? ParentId { get; }

        public ITrackingScope CreateScope(Guid id, string? name) => _context.ScopeFactory.CreateScope(id, name, Id);

        public IManualTracker CreateManualTracker(Guid id) => _context.TrackerFactory.CreateManualTracker(id, Id);

        public IAutoTracker CreateAutoTracker(Guid id) => _context.TrackerFactory.CreateAutoSyncTracker(id, this);
    }
}