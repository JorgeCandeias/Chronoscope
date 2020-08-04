using System;

namespace Chronoscope.Core
{
    public interface ICreateScope
    {
        ITaskScope CreateScope(string name);

        ITaskScope CreateScope(Guid id, string name);
    }
}