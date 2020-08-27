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
        string? Name { get; }

        /// <summary>
        /// The unique identifier of the parent scope, if any.
        /// </summary>
        Guid? ParentId { get; }

        /// <summary>
        /// Creates a new manual tracker under this scope.
        /// </summary>
        IManualTracker CreateManualTracker(Guid id);
    }

    /// <summary>
    /// User friendly extensions for implementers of <see cref="ITrackingScope"/>.
    /// </summary>
    public static class TrackingScopeExtensions
    {
        /// <summary>
        /// Creates a new manual tracker under this scope with an automatic id.
        /// </summary>
        /// <param name="scope">The scope under which to create the new tracker.</param>
        /// <returns>The new tracker in a stopped state.</returns>
        public static IManualTracker CreateManualTracker(this ITrackingScope scope)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateManualTracker(Guid.NewGuid());
        }

        /// <summary>
        /// Creates and starts a new manual tracker under this scope with the specified id.
        /// </summary>
        /// <param name="scope">The scope under which to create the new tracker.</param>
        /// <param name="id">The unique id of the new tracker.</param>
        /// <returns>The new tracker in a running state.</returns>
        public static IManualTracker StartManualTracker(this ITrackingScope scope, Guid id)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            var tracker = scope.CreateManualTracker(id);
            tracker.Start();
            return tracker;
        }

        /// <summary>
        /// Creates and starts a new manual tracker under this scope with an automatic id.
        /// </summary>
        /// <param name="scope">The scope under which to create the new tracker.</param>
        /// <returns>The new tracker in a running state.</returns>
        public static IManualTracker StartManualTracker(this ITrackingScope scope)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            var tracker = scope.CreateManualTracker();
            tracker.Start();
            return tracker;
        }
    }
}