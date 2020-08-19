using Chronoscope;
using System;

namespace Microsoft.Extensions.Hosting
{
    /// <summary>
    /// Extensions for easy integration of chronoscope with the generic host builder.
    /// </summary>
    public static class ChronoScopeHostBuilderExtensions
    {
        /// <summary>
        /// Identifies the current chronoscope builder in the target host builder.
        /// </summary>
        private const string BuilderKey = nameof(ChronoscopeBuilder);

        /// <summary>
        /// Adds and configures chronoscope services on the host builder.
        /// Multiple calls are cumulative.
        /// </summary>
        /// <param name="builder">The builder to extend.</param>
        /// <param name="configure">The configuration action to apply</param>
        /// <returns>The same host builder to allow chaining.</returns>
        public static IHostBuilder UseChronoscope(this IHostBuilder builder, Action<HostBuilderContext, IChronoscopeBuilder> configure)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));

            ChronoscopeBuilder chrono;
            if (builder.Properties.TryGetValue(BuilderKey, out var value))
            {
                chrono = (ChronoscopeBuilder)value;
            }
            else
            {
                builder.Properties[BuilderKey] = chrono = new ChronoscopeBuilder(builder);
            }

            chrono.ConfigureChronoscope(configure);

            return builder;
        }
    }
}