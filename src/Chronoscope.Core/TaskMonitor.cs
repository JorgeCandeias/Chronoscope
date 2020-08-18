using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITaskMonitor"/>.
    /// </summary>
    internal class TaskMonitor : ITaskMonitor
    {
        private readonly ITaskScopeFactory _factory;

        public TaskMonitor(ITaskScopeFactory factory)
        {
            _factory = factory;
        }

        public ITaskScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, null);
    }
}