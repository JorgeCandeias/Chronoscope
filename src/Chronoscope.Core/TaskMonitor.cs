using System;

namespace Chronoscope.Core
{
    internal class TaskMonitor : ITaskMonitor
    {
        private readonly ITaskScopeFactory _factory;

        public TaskMonitor(ITaskScopeFactory factory)
        {
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public ITaskScope CreateScope(string name)
        {
            return _factory.CreateScope(name, null);
        }

        public ITaskScope CreateScope(Guid id, string name)
        {
            return _factory.CreateScope(id, name, null);
        }
    }
}