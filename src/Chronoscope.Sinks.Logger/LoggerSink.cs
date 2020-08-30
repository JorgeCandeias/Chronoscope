using Chronoscope.Events;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace Chronoscope.Sinks.Logger
{
    /// <summary>
    /// Implementation of <see cref="ITrackingSink"/> that writes events to the logging infrastructure.
    /// </summary>
    internal class LoggerSink : ITrackingSink
    {
        private readonly ILogger<LoggerSink> _logger;

        public LoggerSink(ILogger<LoggerSink> logger, IOptions<LoggerSinkOptions> options)
        {
            // grab services
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // we only need this now
            var opt = options?.Value ?? throw new ArgumentNullException(nameof(options));

            // create the high-performance logging delegates
            _logScopeCreated = LoggerMessage.Define<Guid, DateTimeOffset>(LogLevel.Information, new EventId(opt.ScopeCreatedEventOptions.EventId, opt.ScopeCreatedEventOptions.EventName), opt.ScopeCreatedEventOptions.MessageFormat);
            _logTrackerCreated = LoggerMessage.Define<Guid, Guid, DateTimeOffset>(LogLevel.Information, new EventId(opt.TrackerCreatedEventOptions.EventId, opt.TrackerCreatedEventOptions.EventName), opt.TrackerCreatedEventOptions.MessageFormat);
            _logTrackerStarted = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan>(LogLevel.Information, new EventId(opt.TrackerStartedEventOptions.EventId, opt.TrackerStartedEventOptions.EventName), opt.TrackerStartedEventOptions.MessageFormat);
            _logTrackerStopped = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan>(LogLevel.Information, new EventId(opt.TrackerStoppedEventOptions.EventId, opt.TrackerStoppedEventOptions.EventName), opt.TrackerStoppedEventOptions.MessageFormat);
            _logTrackerCompleted = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan>(LogLevel.Information, new EventId(opt.TrackerCompletedEventOptions.EventId, opt.TrackerCompletedEventOptions.EventName), opt.TrackerCompletedEventOptions.MessageFormat);
            _logTrackerFaulted = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan>(LogLevel.Error, new EventId(opt.TrackerFaultedEventOptions.EventId, opt.TrackerFaultedEventOptions.EventName), opt.TrackerFaultedEventOptions.MessageFormat);
            _logTrackerCancelled = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan>(LogLevel.Error, new EventId(opt.TrackerCancelledEventOptions.EventId, opt.TrackerCancelledEventOptions.EventName), opt.TrackerCancelledEventOptions.MessageFormat);
            _logTrackerWarning = LoggerMessage.Define<Guid, Guid, DateTimeOffset, TimeSpan, string>(LogLevel.Warning, new EventId(opt.TrackerWarningEventOptions.EventId, opt.TrackerWarningEventOptions.EventName), opt.TrackerWarningEventOptions.MessageFormat);
        }

        #region High-Performance Logging Delegates

        private readonly Action<ILogger, Guid, DateTimeOffset, Exception?> _logScopeCreated;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, Exception?> _logTrackerCreated;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, Exception?> _logTrackerStarted;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, Exception?> _logTrackerStopped;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, Exception?> _logTrackerCompleted;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, Exception?> _logTrackerFaulted;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, Exception?> _logTrackerCancelled;
        private readonly Action<ILogger, Guid, Guid, DateTimeOffset, TimeSpan, string, Exception?> _logTrackerWarning;

        #endregion High-Performance Logging Delegates

        public void Sink(ITrackingEvent trackingEvent)
        {
            if (trackingEvent is null) throw new ArgumentNullException(nameof(trackingEvent));

            switch (trackingEvent)
            {
                case IScopeCreatedEvent e:
                    _logScopeCreated(_logger, e.ScopeId, e.Timestamp, null);
                    break;

                case ITrackerCreatedEvent e:
                    _logTrackerCreated(_logger, e.ScopeId, e.TrackerId, e.Timestamp, null);
                    break;

                case ITrackerStartedEvent e:
                    _logTrackerStarted(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, null);
                    break;

                case ITrackerStoppedEvent e:
                    _logTrackerStopped(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, null);
                    break;

                case ITrackerCompletedEvent e:
                    _logTrackerCompleted(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, null);
                    break;

                case ITrackerFaultedEvent e:
                    _logTrackerFaulted(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, e.Exception);
                    break;

                case ITrackerCancelledEvent e:
                    _logTrackerCancelled(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, e.Exception);
                    break;

                case ITrackerWarningEvent e:
                    _logTrackerWarning(_logger, e.ScopeId, e.TrackerId, e.Timestamp, e.Elapsed, e.Message, e.Exception);
                    break;

                default:
                    throw new InvalidOperationException($"TrackingEvent of type '{trackingEvent.GetType().Name}' is not supported by this sink");
            }
        }
    }
}