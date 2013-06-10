using System.Collections.Generic;

namespace Sketch.Core.Infrastructure
{
    public interface IEventSourced
    {
        IList<IEvent> Events { get; }
    }
}