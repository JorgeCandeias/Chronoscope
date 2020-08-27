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
            _logScopeCreatedAction = LoggerMessage.Define<Guid, DateTimeOffset>(LogLevel.Information, new EventId(opt.ScopeCreatedEventId, opt.ScopeCreatedEventName), opt.ScopeCreatedMessageFormat);
        }

        #region High-Performance Logging Delegates

        private readonly Action<ILogger, Guid, DateTimeOffset, Exception> _logScopeCreatedAction;

        #endregion High-Performance Logging Delegates

        public void Sink(ITrackingEvent trackingEvent)
        {
            if (trackingEvent is null) throw new ArgumentNullException(nameof(trackingEvent));

            switch (trackingEvent)
            {
                case IScopeCreatedEvent sce:
                    _logScopeCreatedAction(_logger, sce.ScopeId, sce.Timestamp, null);
                    break;

                default:
                    throw new NotSupportedException();
            }
        }
    }
}