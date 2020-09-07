using Chronoscope.Events;
using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeFaultingTrackingSink : ITrackingSink
    {
        private readonly Exception _exception;

        public FakeFaultingTrackingSink(Exception exception)
        {
            _exception = exception;
        }

        public void Sink(ITrackingEvent trackingEvent)
        {
            throw _exception;
        }
    }
}