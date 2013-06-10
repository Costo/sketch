using System;
using Sketch.Core.Domain;

namespace Sketch.Core.Infrastructure
{
    public class DomainEvent: IEvent
    {
        public Guid SourceId { get; set; }
        public int Version { get; set; }
    }
}