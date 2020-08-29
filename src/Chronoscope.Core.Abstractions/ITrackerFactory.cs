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
        IManualTracker CreateManualTracker(Guid id, Guid scopeId);

        /// <summary>
        /// Creates a new automatic synchronous tracker under this scope.
        /// </summary>
        IAutoTracker CreateAutoSyncTracker(Guid id, ITrackingScope scope);
    }
}