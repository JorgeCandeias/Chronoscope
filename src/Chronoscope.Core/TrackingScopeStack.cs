using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;

namespace Chronoscope
{
    public class TrackingScopeStack : ITrackingScopeStack
    {
        private readonly AsyncLocal<ImmutableStack<ITrackingScope>> _stack = new AsyncLocal<ImmutableStack<ITrackingScope>>();

        public TrackingScopeStack()
        {
            _stack.Value = ImmutableStack<ITrackingScope>.Empty;
        }

        public ITrackingScope Peek()
        {
            return _stack.Value.Peek();
        }

        public IEnumerable<ITrackingScope> PeekAll()
        {
            return _stack.Value;
        }

        public void Push(ITrackingScope scope)
        {
            _stack.Value = _stack.Value.Push(scope);
        }
    }
}