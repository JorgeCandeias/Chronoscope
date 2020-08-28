using Chronoscope.Events;
using Chronoscope.Properties;
using System;
using System.Threading;

namespace Chronoscope
{
    internal class AutoSyncTracker : ManualTracker, IAutoSyncTracker
    {
        private readonly ITrackingScope _scope;

        public AutoSyncTracker(ITrackerStopwatch watch, ITrackingEventFactory trackingEventFactory, ITrackingSinks sink, ISystemClock clock, Guid id, ITrackingScope scope)
            : base(watch, trackingEventFactory, sink, clock, id, scope.Id)
        {
            _scope = scope;
        }

        private int _tracking;

        private void AllowTrackingOnce()
        {
            if (Interlocked.CompareExchange(ref _tracking, 1, 0) == 0)
            {
                return;
            }

            throw new InvalidOperationException(Resources.Exception_ThisTrackerIsAlreadyTrackingAnotherAction);
        }

        public TResult Track<TResult>(Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            if (workload is null) throw new ArgumentNullException(nameof(workload));

            AllowTrackingOnce();
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
    }
}