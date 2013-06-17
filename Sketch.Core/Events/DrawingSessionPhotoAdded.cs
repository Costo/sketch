using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class DrawingSessionPhotoAdded: DomainEvent
    {
        public string ImageUrl { get; set; }

        public TimeSpan Duration { get; set; }

        public int Order { get; set; }
    }
}