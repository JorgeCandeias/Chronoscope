using System;

namespace Chronoscope.Events
{
    /// <summary>
    /// The common interface for a sink event as consumed by a <see cref="ITrackingSink"/>.
    /// </summary>
    public interface ITrackingEvent
    {
        /// <summary>
        /// The id of the scope that generated this event.
        /// </summary>
        Guid ScopeId { get; }

        /// <summary>
        /// The timestamp at which this event was generated.
        /// </summary>
        DateTimeOffset Timestamp { get; }
    }
}