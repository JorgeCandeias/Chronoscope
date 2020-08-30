using Chronoscope.Events;
using System.Collections.Generic;

namespace Chronoscope.Tests.Fakes
{
    public class FakeSinks : ITrackingSinks
    {
        public void Sink(ITrackingEvent trackingEvent)
        {
            Events.Add(trackingEvent);
        }

        public IList<ITrackingEvent> Events { get; } = new List<ITrackingEvent>();
    }
}