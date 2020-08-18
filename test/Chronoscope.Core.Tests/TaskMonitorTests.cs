using Moq;
using System;
using Xunit;

namespace Chronoscope.Core.Tests
{
    public class TaskMonitorTests
    {
        [Fact]
        public void CreatesScope()
        {
            // arrange
            var id = Guid.NewGuid();
            var name = Guid.NewGuid().ToString();
            var scope = Mock.Of<ITaskScope>(x => x.Id == id && x.Name == name);
            var factory = Mock.Of<ITaskScopeFactory>(x => x.CreateScope(id, name, null) == scope);
            var monitor = new TaskMonitor(factory);

            // act
            var result = monitor.CreateScope(id, name);

            // assert
            Assert.Same(scope, result);
        }
    }
}