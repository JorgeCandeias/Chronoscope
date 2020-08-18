using Microsoft.Extensions.Logging;
using System;

namespace Chronoscope
{
    internal class TaskScope : ITaskScope
    {
        private readonly ILogger<TaskScope> _logger;
        private readonly ITaskScopeFactory _factory;

        private readonly Guid _id;
        private readonly string? _name;
        private readonly Guid? _parentId;

        internal TaskScope(ILogger<TaskScope> logger, ITaskScopeFactory factory, Guid id, string? name, Guid? parentId)
        {
            _logger = logger;
            _factory = factory;
            _id = id;
            _name = name;
            _parentId = parentId;
        }

        public ITaskScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, _id);
    }
}