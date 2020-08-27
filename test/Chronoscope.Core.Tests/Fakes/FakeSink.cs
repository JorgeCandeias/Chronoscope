using Chronoscope.Events;
using System.Collections.Generic;

namespace Chronoscope.Core.Tests.Fakes
{
    internal class FakeSink : ITrackingSink
    {
        public void Sink(ITrackingEvent trackingEvent)
        {
            Events.Add(trackingEvent);
        }

        public IList<ITrackingEvent> Events { get; } = new List<ITrackingEvent>();
    }
}