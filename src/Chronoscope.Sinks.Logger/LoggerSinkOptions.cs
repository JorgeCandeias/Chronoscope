using System.ComponentModel.DataAnnotations;

namespace Chronoscope.Sinks.Logger
{
    /// <summary>
    /// Options for <see cref="LoggerSink"/>.
    /// </summary>
    public class LoggerSinkOptions
    {
        [Required]
        public LoggerMessageOptions ScopeCreatedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 1,
            EventName = "ScopeCreated",
            MessageFormat = "Scope {ScopeId} was created at {Timestamp}"
        };

        [Required]
        public LoggerMessageOptions TrackerCreatedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 2,
            EventName = "TrackerCreated",
            MessageFormat = "Scope {ScopeId} created tracker {TrakerId} at {Timestamp}"
        };

        [Required]
        public LoggerMessageOptions TrackerStartedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 3,
            EventName = "TrackerStarted",
            MessageFormat = "Scope {ScopeId} started tracker {TrackerId} as {Timestamp} with {Elapsed} elapsed"
        };

        [Required]
        public LoggerMessageOptions TrackerStoppedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 4,
            EventName = "TrackerStopped",
            MessageFormat = "Scope {ScopeId} stopped tracker {TrackerId} as {Timestamp} with {Elapsed} elapsed"
        };
    }

    public class LoggerMessageOptions
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EventId { get; set; }

        [Required]
        public string MessageFormat { get; set; }

        [Required]
        public string EventName { get; set; }
    }
}