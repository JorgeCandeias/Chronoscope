using Microsoft.Extensions.Logging;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITaskScopeFactory"/>.
    /// </summary>
    internal class TaskScopeFactory : ITaskScopeFactory
    {
        private readonly ILoggerFactory _loggerFactory;

        public TaskScopeFactory(ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
        }

        public ITaskScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            var logger = _loggerFactory.CreateLogger<TaskScope>();

            return new TaskScope(_loggerFactory.CreateLogger<TaskScope>(), this, id, name, parentId);
        }
    }
}