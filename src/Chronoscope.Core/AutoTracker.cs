using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace Chronoscope
{
    [SuppressMessage("Minor Code Smell", "S4136:Method overloads should be grouped together")]
    internal class AutoTracker : ManualTracker, IAutoTracker
    {
        private readonly ITrackingScope _scope;

        public AutoTracker(IChronoscopeContext context, Guid id, ITrackingScope scope)
            : base(context, id, scope.Id)
        {
            _scope = scope;
        }

        #region Void Sync Tracking

        public void Track(Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            Start();

            try
            {
                workload(_scope, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                Cancel(ex);
                throw;
            }
            catch (Exception ex)
            {
                Fault(ex);
                throw;
            }

            Complete();
        }

        #endregion Void Sync Tracking

        #region Result Sync Tracking

        public TResult Track<TResult>(Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            Start();

            TResult result;
            try
            {
                result = workload(_scope, cancellationToken);
            }
            catch (OperationCanceledException ex)
            {
                Cancel(ex);
                throw;
            }
            catch (Exception ex)
            {
                Fault(ex);
                throw;
            }

            Complete();
            return result;
        }

        #endregion Result Sync Tracking

        #region Void Async Tracking

        public Task TrackAsync(Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default)
        {
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return InnerTrackAsync(workload, cancellationToken);
        }

        private async Task InnerTrackAsync(Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default)
        {
            Start();

            try
            {
                await workload(_scope, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                Cancel(ex);
                throw;
            }
            catch (Exception ex)
            {
                Fault(ex);
                throw;
            }

            Complete();
        }

        #endregion Void Async Tracking

        #region Result Async Tracking

        public Task<TResult> TrackAsync<TResult>(Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default)
        {
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            return InnerTrackAsync(workload, cancellationToken);
        }

        private async Task<TResult> InnerTrackAsync<TResult>(Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default)
        {
            Start();

            TResult result;
            try
            {
                result = await workload(_scope, cancellationToken).ConfigureAwait(false);
            }
            catch (OperationCanceledException ex)
            {
                Cancel(ex);
                throw;
            }
            catch (Exception ex)
            {
                Fault(ex);
                throw;
            }

            Complete();
            return result;
        }

        #endregion Result Async Tracking
    }
}