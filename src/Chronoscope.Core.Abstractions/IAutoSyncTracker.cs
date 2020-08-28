using System;
using System.Threading;

namespace Chronoscope
{
    /// <summary>
    /// A tracker that automatically monitors synchronous workloads.
    /// </summary>
    public interface IAutoSyncTracker
    {
        /// <summary>
        /// Automatically and transparently measures the specified workload.
        /// </summary>
        /// <typeparam name="TResult">The result of the workload.</typeparam>
        /// <param name="workload">The workload to measure.</param>
        /// <param name="cancellationToken">Allows cancelling the workload.</param>
        TResult Track<TResult>(Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Productivity extensions for implementers of <see cref="IAutoSyncTracker"/>.
    /// </summary>
    public static class AutoSyncTrackerExtensions
    {
        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this IAutoSyncTracker tracker, Func<TResult> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - result only
            return tracker.Track((scope, token) => workload(), CancellationToken.None);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this IAutoSyncTracker tracker, Func<ITrackingScope, TResult> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - result and scope only
            return tracker.Track((scope, token) => workload(scope), CancellationToken.None);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static TResult Track<TResult>(this IAutoSyncTracker tracker, Func<CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - result and token only
            return tracker.Track((scope, token) => workload(token), cancellationToken);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static void Track(this IAutoSyncTracker tracker, Action workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - void only
            tracker.Track((scope, token) =>
            {
                workload();
                return true;
            }, CancellationToken.None);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static void Track(this IAutoSyncTracker tracker, Action<ITrackingScope> workload)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - void and scope only
            tracker.Track((scope, token) =>
            {
                workload(scope);
                return true;
            }, CancellationToken.None);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static void Track(this IAutoSyncTracker tracker, Action<CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - void and token only
            tracker.Track((scope, token) =>
            {
                workload(token);
                return true;
            }, cancellationToken);
        }

        /// <inheritdoc cref="IAutoSyncTracker.Track{TResult}(Func{ITrackingScope, CancellationToken, TResult}, CancellationToken)"/>
        public static void Track(this IAutoSyncTracker tracker, Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            if (tracker is null) throw new ArgumentNullException(nameof(tracker));
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            // variation - void and scope and token
            tracker.Track((scope, token) =>
            {
                workload(scope, token);
                return true;
            }, cancellationToken);
        }
    }
}