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
            MessageFormat = "Scope {ScopeId} started tracker {TrackerId} at {Timestamp} with elapsed time of {Elapsed}"
        };

        [Required]
        public LoggerMessageOptions TrackerStoppedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 4,
            EventName = "TrackerStopped",
            MessageFormat = "Scope {ScopeId} stopped tracker {TrackerId} at {Timestamp} with elapsed of {Elapsed}"
        };

        [Required]
        public LoggerMessageOptions TrackerCompletedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 5,
            EventName = "TrackerCompleted",
            MessageFormat = "Scope {ScopeId} completed tracker {TrackerId} at {Timestamp} with elapsed time of {Elapsed}"
        };

        [Required]
        public LoggerMessageOptions TrackerFaultedEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 6,
            EventName = "TrackerFaulted",
            MessageFormat = "Scope {ScopeId} faulted tracker {TrackerId} at {Timestamp} with elapsed time of {Elapsed}"
        };

        [Required]
        public LoggerMessageOptions TrackerCancelledEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 7,
            EventName = "TrackerCancelled",
            MessageFormat = "Scope {ScopeId} cancelled tracker {TrackerId} at {Timestamp} with elapsed time of {Elapsed}"
        };

        [Required]
        public LoggerMessageOptions TrackerWarningEventOptions { get; set; } = new LoggerMessageOptions
        {
            EventId = 8,
            EventName = "TrackerWarning",
            MessageFormat = "Scope {ScopeId} issued warning for tracker {TrackerId} at {Timestamp} with elapsed time of {Elapsed} with message {Message}"
        };
    }

    public class LoggerMessageOptions
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int EventId { get; set; }

        [Required]
        public string MessageFormat { get; set; } = string.Empty;

        [Required]
        public string EventName { get; set; } = string.Empty;
    }
}