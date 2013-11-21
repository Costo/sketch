using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class DrawingSessionPhotoAdded: DomainEvent
    {
        public Guid StockPhotoId { get; set; }
        public string PageUrl { get; set; }

        public TimeSpan Duration { get; set; }

        public int Index { get; set; }

    }
}