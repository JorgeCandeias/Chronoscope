using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a way to create trackers.
    /// </summary>
    public interface ITrackerFactory
    {
        /// <summary>
        /// Creates a new manual tracker under this scope.
        /// </summary>
        IManualTracker CreateTracker(Guid id, Guid scopeId);
    }
}