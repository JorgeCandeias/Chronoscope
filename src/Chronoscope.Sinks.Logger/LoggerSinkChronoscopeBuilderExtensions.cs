using Chronoscope.Sinks.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Dependency injection extensions for <see cref="LoggerSink"/>.
    /// </summary>
    public static class LoggerSinkChronoscopeBuilderExtensions
    {
        /// <summary>
        /// Adds the <see cref="LoggerSink"/> to the specified <see cref="IChronoscopeBuilder"/> with the specified options.
        /// </summary>
        /// <param name="builder">The builder to add the sink to.</param>
        /// <param name="configure">The action to configure the sink options.</param>
        /// <returns>The specified builder to allow chaining.</returns>
        public static IChronoscopeBuilder AddLoggerSink(this IChronoscopeBuilder builder, Action<LoggerSinkOptions> configure)
        {
            if (builder is null) throw new ArgumentNullException(nameof(builder));
            if (configure is null) throw new ArgumentNullException(nameof(builder));

            return builder.ConfigureServices(services =>
            {
                services
                    .AddSingleton<ITrackingSink, LoggerSink>()
                    .AddOptions<LoggerSinkOptions>().Configure(configure).ValidateDataAnnotations();
            });
        }

        /// <summary>
        /// Adds the <see cref="LoggerSink"/> to the specified <see cref="IChronoscopeBuilder"/>.
        /// </summary>
        /// <param name="builder">The builder to add the sink to.</param>
        /// <returns>The specified builder to allow chaining.</returns>
        public static IChronoscopeBuilder AddLoggerSink(this IChronoscopeBuilder builder) => builder.AddLoggerSink(options => { });
    }
}