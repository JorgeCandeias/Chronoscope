using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IChronoscope"/>.
    /// </summary>
    internal class Chronoscope : IChronoscope
    {
        private readonly ITrackingScopeFactory _factory;

        public Chronoscope(ITrackingScopeFactory factory)
        {
            _factory = factory;
        }

        public ITrackingScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, null);
    }
}