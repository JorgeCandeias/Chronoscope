using System;

namespace Chronoscope
{
    /// <summary>
    /// Common interface for all trackers.
    /// </summary>
    public interface ITracker
    {
        /// <summary>
        /// Unique identifier of this tracker.
        /// </summary>
        Guid Id { get; }
    }
}