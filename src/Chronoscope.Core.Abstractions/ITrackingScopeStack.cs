using System.Collections.Generic;

namespace Chronoscope
{
    public interface ITrackingScopeStack
    {
        void Push(ITrackingScope scope);

        ITrackingScope Peek();

        IEnumerable<ITrackingScope> PeekAll();
    }
}