using System;
using System.Threading;
using System.Threading.Tasks;

namespace Chronoscope.Tests.Fakes
{
    public class FakeAutoTracker : IAutoTracker
    {
        public Guid Id { get; set; }

        public TimeSpan Elapsed { get; set; }

        public ITrackingScope Scope { get; set; }

        public void Track(Action<ITrackingScope, CancellationToken> workload, CancellationToken cancellationToken = default)
        {
            workload(Scope, cancellationToken);
        }

        public TResult Track<TResult>(Func<ITrackingScope, CancellationToken, TResult> workload, CancellationToken cancellationToken = default)
        {
            return workload(Scope, cancellationToken);
        }

        public Task TrackAsync(Func<ITrackingScope, CancellationToken, Task> workload, CancellationToken cancellationToken = default)
        {
            return workload(Scope, cancellationToken);
        }

        public Task<TResult> TrackAsync<TResult>(Func<ITrackingScope, CancellationToken, Task<TResult>> workload, CancellationToken cancellationToken = default)
        {
            return workload(Scope, cancellationToken);
        }
    }
}