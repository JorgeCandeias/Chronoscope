using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingScopeFactory"/>.
    /// </summary>
    internal class TrackingScopeFactory : ITrackingScopeFactory
    {
        private readonly IChronoscopeContext _context;

        public TrackingScopeFactory(IChronoscopeContext context)
        {
            _context = context;
        }

        public ITrackingScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            return new TrackingScope(_context, id, name, parentId);
        }
    }
}