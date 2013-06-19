using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sketch.Core.Infrastructure.Storage
{
    [Table("Events", Schema = "Events")]
    public class Event
    {
        [Key, Column(Order = 1)]
        public Guid AggregateId { get; set; }
        [Key, Column(Order = 2)]
        public int Version { get; set; }

        public string Payload { get; set; }
        public string EventType { get; set; }
        public string CorrelationId { get; set; }
    }
}
