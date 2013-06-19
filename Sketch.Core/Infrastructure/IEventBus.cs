using System.Collections.Generic;

namespace Sketch.Core.Infrastructure
{
    public interface IEventBus
    {
        void Publish(IEvent @event);
        void Publish(IEnumerable<IEvent> events);
    }
}