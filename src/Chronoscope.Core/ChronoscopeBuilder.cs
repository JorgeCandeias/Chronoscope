using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="IChronoscopeBuilder"/>.
    /// </summary>
    internal class ChronoscopeBuilder : IChronoscopeBuilder
    {
        public ChronoscopeBuilder(IHostBuilder builder)
        {
            // wire up chronoscope configurations
            builder.ConfigureServices((context, services) =>
            {
                // chronoscope configuration actions will populate the service configuration actions
                foreach (var configure in _chronoscopeConfigurators)
                {
                    configure(context, this);
                }

                // remove the configurators to allow gc
                _chronoscopeConfigurators.Clear();
            });

            // wire up service configurations
            builder.ConfigureServices((context, services) =>
            {
                foreach (var configure in _serviceConfigurators)
                {
                    configure(context, services);
                }

                // remove the configurators to allow gc
                _serviceConfigurators.Clear();
            });
        }

        private readonly List<Action<HostBuilderContext, IChronoscopeBuilder>> _chronoscopeConfigurators = new List<Action<HostBuilderContext, IChronoscopeBuilder>>();
        private readonly List<Action<HostBuilderContext, IServiceCollection>> _serviceConfigurators = new List<Action<HostBuilderContext, IServiceCollection>>();

        public IChronoscopeBuilder ConfigureChronoscope(Action<HostBuilderContext, IChronoscopeBuilder> configure)
        {
            if (configure is null) throw new ArgumentNullException(nameof(configure));

            _chronoscopeConfigurators.Add(configure);

            return this;
        }

        public IChronoscopeBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configure)
        {
            if (configure is null) throw new ArgumentNullException(nameof(configure));

            _serviceConfigurators.Add(configure);

            return this;
        }
    }
}