using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Allows adding chronoscope specific services to the underlying host builder.
    /// </summary>
    public interface IChronoscopeBuilder
    {
        /// <summary>
        /// Allows adding services to the underlying host.
        /// </summary>
        /// <param name="configure">The callback to use for adding services.</param>
        /// <returns>The same builder instance to allow chaining.</returns>
        public IChronoscopeBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configure);
    }

    /// <summary>
    /// Quality-of-life extension methods for <see cref="IChronoscopeBuilder"/>.
    /// </summary>
    public static class ChronoscopeBuilderExtensions
    {
        /// <summary>
        /// Allows adding services to the underlying host.
        /// </summary>
        /// <param name="builder">The builder to extend.</param>
        /// <param name="configure">The callback to use for adding services.</param>
        /// <returns>The same builder instance to allow chaining.</returns>
        public static IChronoscopeBuilder ConfigureServices(this IChronoscopeBuilder builder, Action<IServiceCollection> configure)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));
            if (configure is null) throw new ArgumentNullException(nameof(configure));

            return builder.ConfigureServices((context, services) => configure(services));
        }
    }
}