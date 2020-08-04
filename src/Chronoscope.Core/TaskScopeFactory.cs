using Microsoft.Extensions.Logging;
using System;

namespace Chronoscope.Core
{
    internal class TaskScopeFactory : ITaskScopeFactory
    {
        private readonly ILogger<TaskScope> _scopeLogger;

        public TaskScopeFactory(ILogger<TaskScope> scopeLogger)
        {
            _scopeLogger = scopeLogger ?? throw new ArgumentNullException(nameof(scopeLogger));
        }

        public ITaskScope CreateScope(string name, ITaskScope? parent) => CreateScope(Guid.NewGuid(), name, parent);

        public ITaskScope CreateScope(Guid id, string name, ITaskScope? parent) => new TaskScope(_scopeLogger, this, id, name, parent);
    }
}