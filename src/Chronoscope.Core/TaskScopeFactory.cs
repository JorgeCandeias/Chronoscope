using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace Chronoscope
{
    /// <summary>
    /// Default implementation of <see cref="ITaskScopeFactory"/>.
    /// </summary>
    internal class TaskScopeFactory : ITaskScopeFactory
    {
        private readonly IServiceProvider _provider;

        public TaskScopeFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        public ITaskScope CreateScope(Guid id, string? name, Guid? parentId)
        {
            var options = _provider.GetRequiredService<IOptions<ChronoscopeOptions>>();

            return new TaskScope(options, this, id, name, parentId);
        }
    }
}