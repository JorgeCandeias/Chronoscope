using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingScopeFactory"/>.
    /// </summary>
    internal class TrackingScopeFactory : ITrackingScopeFactory
    {
        private readonly IServiceProvider _provider;

        public TrackingScopeFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public ITrackingScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            var options = _provider.GetRequiredService<IOptions<ChronoscopeOptions>>();
            var context = _provider.GetRequiredService<IChronoscopeContext>();

            return new TrackingScope(options, context, id, name, parentId);
        }
    }
}