using System;

namespace Chronoscope
{
    /// <summary>
    /// Defines a tracking scope that can measure task lifetime properties.
    /// </summary>
    public interface ITrackingScope : ICreateScope
    {
        /// <summary>
        /// The unique identifier of this scope.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// The name of this scope.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The unique identifier of the parent scope, if any.
        /// </summary>
        Guid? ParentId { get; }

        /// <summary>
        /// Creates a new manual tracker under this scope.
        /// </summary>
        IManualTracker CreateTracker();
    }
}