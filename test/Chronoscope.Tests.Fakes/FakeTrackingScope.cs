using System;

namespace Chronoscope.Tests.Fakes
{
    public class FakeTrackingScope : ITrackingScope
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ParentId { get; set; }

        public IAutoTracker CreateAutoTracker(Guid id)
        {
            return new FakeAutoTracker
            {
                Id = id,
                Scope = this
            };
        }

        public IManualTracker CreateManualTracker(Guid id)
        {
            throw new NotImplementedException();
        }

        public ITrackingScope CreateScope(Guid id, string name)
        {
            return new FakeTrackingScope
            {
                Id = id,
                Name = name,
                ParentId = Id
            };
        }
    }
}