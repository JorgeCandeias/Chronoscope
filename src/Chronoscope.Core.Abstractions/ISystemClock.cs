using System;

namespace Chronoscope
{
    /// <summary>
    /// Abstracts away static calls to <see cref="DateTimeOffset"/> to simplify unit testing.
    /// </summary>
    public interface ISystemClock
    {
        DateTimeOffset Now { get; }
    }
}