using Microsoft.Extensions.Logging;
using System;

namespace Chronoscope.Core
{
    internal class TaskScope : ITaskScope
    {
        private readonly ILogger<TaskScope> _logger;
        private readonly ITaskScopeFactory _factory;

        private readonly Guid _id;
        private readonly string _name;
        private readonly ITaskScope? _parent;

        public TaskScope(ILogger<TaskScope> logger, ITaskScopeFactory factory, Guid id, string name, ITaskScope? parent)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _factory = factory ?? throw new ArgumentNullException(nameof(factory));

            _id = id;
            _name = name ?? throw new ArgumentNullException(nameof(name));
            _parent = parent;
        }

        public ITaskScope CreateScope(string name) => _factory.CreateScope(name, this);

        public ITaskScope CreateScope(Guid id, string name) => _factory.CreateScope(id, name, this);
    }
}