using Chronoscope.Events;

namespace Chronoscope
{
    /// <summary>
    /// Represents a target sink that can receive tracking events and handle them as appropriate.
    /// </summary>
    public interface ITrackingSink
    {
        /// <summary>
        /// Pushes the specified event to this sink.
        /// </summary>
        /// <param name="trackingEvent">The tracking event to push.</param>
        void Sink(ITrackingEvent trackingEvent);
    }
}