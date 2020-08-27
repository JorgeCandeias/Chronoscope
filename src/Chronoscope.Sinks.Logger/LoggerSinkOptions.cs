using System.ComponentModel.DataAnnotations;

namespace Chronoscope.Sinks.Logger
{
    /// <summary>
    /// Options for <see cref="LoggerSink"/>.
    /// </summary>
    public class LoggerSinkOptions
    {
        /// <summary>
        /// The default logging event id for the scope created event.
        /// </summary>
        public static int DefaultScopeCreatedEventId { get; } = 1;

        /// <summary>
        /// The default logging event name for the scope created event.
        /// </summary>
        public static string DefaultScopeCreatedEventName { get; } = "ScopeCreated";

        /// <summary>
        /// The default logging message format for the scope created event.
        /// </summary>
        public static string DefaultScopeCreatedMessageFormat { get; } = "Scope {ScopeId} was created at {Timestamp}";

        /// <summary>
        /// The logging event id for the scope created event.
        /// </summary>
        [Required]
        [Range(1, int.MaxValue)]
        public int ScopeCreatedEventId { get; } = DefaultScopeCreatedEventId;

        /// <summary>
        /// The message format used for the scope created event.
        /// </summary>
        [Required]
        public string ScopeCreatedMessageFormat { get; } = DefaultScopeCreatedMessageFormat;

        /// <summary>
        /// The name used for the scope creation event.
        /// </summary>
        [Required]
        public string ScopeCreatedEventName { get; } = DefaultScopeCreatedEventName;
    }
}