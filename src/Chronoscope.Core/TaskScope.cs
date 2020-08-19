using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Globalization;

namespace Chronoscope
{
    internal class TaskScope : ITaskScope
    {
        private readonly ILogger<TaskScope> _logger;
        private readonly ITaskScopeFactory _factory;

        internal TaskScope(IOptions<ChronoscopeOptions> options, ILogger<TaskScope> logger, ITaskScopeFactory factory, Guid id, string? name, Guid? parentId)
        {
            _logger = logger;
            _factory = factory;

            Id = id;
            Name = name ?? string.Format(CultureInfo.InvariantCulture, options.Value.DefaultTaskScopeNameFormat, id);
            ParentId = parentId;
        }

        public Guid Id { get; }

        public string Name { get; }

        public Guid? ParentId { get; }

        public ITaskScope CreateScope(Guid id, string? name) => _factory.CreateScope(id, name, Id);
    }
}