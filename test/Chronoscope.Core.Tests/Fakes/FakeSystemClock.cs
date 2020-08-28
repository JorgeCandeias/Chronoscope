using System;

namespace Chronoscope.Core.Tests.Fakes
{
    public class FakeSystemClock : ISystemClock
    {
        public DateTimeOffset Now { get; set; }
    }
}