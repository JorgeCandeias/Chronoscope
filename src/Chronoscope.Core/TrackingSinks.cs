using Chronoscope.Events;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITrackingSinks"/>.
    /// </summary>
    internal class TrackingSinks : ITrackingSinks
    {
        private readonly ILogger _logger;
        private readonly ITrackingSink[] _sinks;

        public TrackingSinks(ILogger<TrackingSinks> logger, IEnumerable<ITrackingSink> sinks)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _sinks = sinks?.ToArray() ?? throw new ArgumentNullException(nameof(sinks));
        }

        private readonly Action<ILogger, Exception> _logErrorAction = LoggerMessage.Define(LogLevel.Error, new EventId(999, "SinkFailed"), "Could not push an event to the configured sink");

        public void Sink(ITrackingEvent trackingEvent)
        {
            foreach (var sink in _sinks)
            {
                try
                {
                    sink.Sink(trackingEvent);
                }
                catch (Exception ex)
                {
                    _logErrorAction(_logger, ex);
                    throw;
                }
            }
        }
    }
}