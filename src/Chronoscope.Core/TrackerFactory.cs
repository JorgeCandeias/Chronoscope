using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackerFactory"/>.
    /// </summary>
    internal class TrackerFactory : ITrackerFactory
    {
        private readonly IServiceProvider _provider;

        public TrackerFactory(IServiceProvider provider)
        {
            _provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public IAutoTracker CreateAutoSyncTracker(Guid id, ITrackingScope scope)
        {
            var context = _provider.GetRequiredService<IChronoscopeContext>();

            return new AutoTracker(context, id, scope);
        }

        public IManualTracker CreateManualTracker(Guid id, Guid scopeId)
        {
            var context = _provider.GetRequiredService<IChronoscopeContext>();

            return new ManualTracker(context, id, scopeId);
        }
    }
}