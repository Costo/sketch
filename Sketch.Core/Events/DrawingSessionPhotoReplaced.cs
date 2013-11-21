using Sketch.Core.Infrastructure;
using System;

namespace Sketch.Core.Events
{
    public class DrawingSessionPhotoReplaced : DomainEvent
    {
        public Guid NewStockPhotoId { get; set; }
        public string NewPageUrl { get; set; }
        public int IndexOfPhoto { get; set; }
    }
}