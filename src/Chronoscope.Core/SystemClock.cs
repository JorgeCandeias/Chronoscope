using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ISystemClock"/>.
    /// </summary>
    internal class SystemClock : ISystemClock
    {
        public DateTimeOffset Now => DateTimeOffset.Now;
    }
}