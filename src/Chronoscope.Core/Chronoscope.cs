using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IChronoscope"/>.
    /// </summary>
    internal class Chronoscope : IChronoscope
    {
        private readonly IChronoscopeContext _context;

        public Chronoscope(IChronoscopeContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public ITrackingScope CreateScope(Guid id, string? name) => _context.ScopeFactory.CreateScope(id, name, null);
    }
}