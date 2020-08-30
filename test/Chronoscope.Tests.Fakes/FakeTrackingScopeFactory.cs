using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackingScopeFactory : ITrackingScopeFactory
    {
        public ITrackingScope CreateScope(Guid id, string name, Guid? parentId)
        {
            return new FakeTrackingScope
            {
                Id = id,
                Name = name,
                ParentId = parentId
            };
        }
    }
}