using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chronoscope
{
    /// <summary>
    /// A tracker that automatically monitors synchronous workloads.
    /// </summary>
    public interface IAutoTracker : ITracker
    {
        /// <summary>
        /// Automatically and transparently measures the specified workload.
        /// </summary>
        /// <param name="workload">The workload to measure.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        void Track(Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Automatically and transparently measures the specified workload.
        /// </summary>
        /// <typeparam name="TResult">The result of the workload.</typeparam>
        /// <param name="workload">The workload to measure.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        TResult Track<TResult>(Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Automatically and transparently measures the specified workload.
        /// </summary>
        /// <param name="workload">The workload to measure.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        Task TrackAsync(Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default);

        /// <summary>
        /// Automatically and transparently measures the specified workload.
        /// </summary>
        /// <typeparam name="TResult">The result of the workload.</typeparam>
        /// <param name="workload">The workload to measure.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        Task<TResult> TrackAsync<TResult>(Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Productivity extensions for implementers of <see cref="IAutoTracker"/>.
    /// </summary>
    public static class AutoTrackerExtensions
    {
        /// <inheritdoc cref="IAutoTracker.Track(Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this IAutoTracker tracker, Action<ITrackingScope> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            tracker.Track((scope, token) => workload(scope));
        }

        /// <inheritdoc cref="IAutoTracker.Track(Action{ITrackingScope, CancellationToken}, CancellationToken)"/>
        public static void Track(this IAutoTracker tracker, Action workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            tracker.Track((scope, token) => workload());
        }

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this IAutoTracker tracker, Func<ITrackingScope, TResult> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.Track((scope, token) => workload(scope));
        }

        /// <inheritdoc cref="IAutoTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this IAutoTracker tracker, Func<TResult> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.Track((scope, token) => workload());
        }

        /// <inheritdoc cref="IAutoTracker.TrackAsync(Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this IAutoTracker tracker, Func<ITrackingScope, Task> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.TrackAsync((scope, token) => workload(scope));
        }

        /// <inheritdoc cref="IAutoTracker.TrackAsync(Func{ITrackingScope, CancellationToken, Task}, CancellationToken)"/>
        public static Task TrackAsync(this IAutoTracker tracker, Func<Task> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.TrackAsync((scope, token) => workload());
        }

        /// <inheritdoc cref="IAutoTracker.TrackAsync{TResult}(Func{ITrackingScope, CancellationToken, Task{TResult}}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this IAutoTracker tracker, Func<ITrackingScope, Task<TResult>> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.TrackAsync((scope, token) => workload(scope));
        }

        /// <inheritdoc cref="IAutoTracker.TrackAsync{TResult}(Func{ITrackingScope, CancellationToken, Task{TResult}}, CancellationToken)"/>
        public static Task<TResult> TrackAsync<TResult>(this IAutoTracker tracker, Func<Task<TResult>> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return tracker.TrackAsync((scope, token) => workload());
        }
    }
}