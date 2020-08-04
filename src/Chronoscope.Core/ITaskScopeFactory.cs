using System;

namespace Chronoscope.Core
{
    internal interface ITaskScopeFactory
    {
        ITaskScope CreateScope(Guid id, string name, ITaskScope? parent);

        ITaskScope CreateScope(string name, ITaskScope? parent);
    }
}