using System;
using System.Threading;

namespace Chronoscope.Tests.Fakes
{
    public sealed class DisposableAction : IDisposable
    {
        private Action _action;

        public DisposableAction(Action action)
        {
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public void Dispose()
        {
            if (_action is null) return;

            Interlocked.Exchange(ref _action, null)?.Invoke();
            GC.SuppressFinalize(this);
        }

        ~DisposableAction()
        {
            Dispose();
        }
    }
}