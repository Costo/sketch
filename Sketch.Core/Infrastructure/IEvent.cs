using System;

namespace Sketch.Core.Infrastructure
{
    public interface IEvent
    {
        Guid SourceId { get; set; }
        int Version { get; set; }
    }
}