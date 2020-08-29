using System;
using System.Threading;
using System.Threading.Tasks;

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

        /// <summary>
        /// Creates a new automatic synchronous tracker under this scope.
        /// </summary>
        IAutoTracker CreateAutoTracker(Guid id);
    }

    /// <summary>
    /// User friendly extensions for implementers of <see cref="ITrackingScope"/>.
    /// </summary>
    public static class TrackingScopeManualTrackerExtensions
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

        /// <inheritdoc cref="StartManualTracker(ITrackingScope, Guid)"/>
        public static IManualTracker StartManualTracker(this ITrackingScope scope)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            var tracker = scope.CreateManualTracker();
            tracker.Start();
            return tracker;
        }
    }

    /// <summary>
    /// User friendly extensions for implementers of <see cref="ITrackingScope"/>.
    /// </summary>
    public static class TrackingScopeAutoTrackerExtensions
    {
        #region Void Sync Tracking With Known Id

        /// <summary>
        /// Creates and starts a new automatic tracker for the specified workload.
        /// </summary>
        /// <param name="scope">The scope under which to create the new tracker.</param>
        /// <param name="id">The unique id of the new tracker.</param>
        /// <param name="workload">The workload to track.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        public static void Track(this ITrackingScope scope, Guid id, Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(id).Track(workload, cancellationToken);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this ITrackingScope scope, Guid id, Action<ITrackingScope> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(id).Track(workload);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this ITrackingScope scope, Guid id, Action workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(id).Track(workload);
        }

        #endregion Void Sync Tracking With Known Id

        #region Void Sync Tracking With Automatic Id

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this ITrackingScope scope, Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(Guid.NewGuid()).Track(workload, cancellationToken);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this ITrackingScope scope, Action<ITrackingScope> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(Guid.NewGuid()).Track(workload);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this ITrackingScope scope, Action workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            scope.CreateAutoTracker(Guid.NewGuid()).Track(workload);
        }

        #endregion Void Sync Tracking With Automatic Id

        #region Result Sync Tracking With Known Id

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Guid id, Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).Track(workload, cancellationToken);
        }

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Guid id, Func<ITrackingScope, TResult> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).Track(workload);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Guid id, Func<TResult> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).Track(workload);
        }

        #endregion Result Sync Tracking With Known Id

        #region Result Sync Tracking With Automatic Id

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).Track(workload, cancellationToken);
        }

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Func<ITrackingScope, TResult> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).Track(workload);
        }

        /// <inheritdoc cref="Track(ITrackingScope, Guid, Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static TResult Track<TResult>(this ITrackingScope scope, Func<TResult> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).Track(workload);
        }

        #endregion Result Sync Tracking With Automatic Id

        #region Void Async Tracking With Known Id

        /// <summary>
        /// Creates and starts a new automatic tracker for the specified workload.
        /// </summary>
        /// <param name="scope">The scope under which to create the new tracker.</param>
        /// <param name="id">The unique id of the new tracker.</param>
        /// <param name="workload">The workload to track.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        public static Task TrackAsync(this ITrackingScope scope, Guid id, Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload, cancellationToken);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this ITrackingScope scope, Guid id, Func<ITrackingScope, Task> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this ITrackingScope scope, Guid id, Func<Task> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload);
        }

        #endregion Void Async Tracking With Known Id

        #region Void Async Tracking With Automatic Id

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this ITrackingScope scope, Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload, cancellationToken);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this ITrackingScope scope, Func<ITrackingScope, Task> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this ITrackingScope scope, Func<Task> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload);
        }

        #endregion Void Async Tracking With Automatic Id

        #region Result Async Tracking With Known Id

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Guid id, Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload, cancellationToken);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Guid id, Func<ITrackingScope, Task<TResult>> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Guid id, Func<Task<TResult>> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(id).TrackAsync(workload);
        }

        #endregion Result Async Tracking With Known Id

        #region Result Async Tracking With Automatic Id

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload, cancellationToken);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Func<ITrackingScope, Task<TResult>> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload);
        }

        /// <inheritdoc cref="TrackAsync(ITrackingScope, Guid, Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this ITrackingScope scope, Func<Task<TResult>> workload)
        {
            if (scope is null) throw new ArgumentNullException(nameof(scope));

            return scope.CreateAutoTracker(Guid.NewGuid()).TrackAsync(workload);
        }

        #endregion Result Async Tracking With Automatic Id
    }
}