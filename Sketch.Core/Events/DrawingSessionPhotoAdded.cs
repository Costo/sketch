using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class DrawingSessionPhotoAdded: DomainEvent
    {
        public string PhotoUrl { get; set; }

        public TimeSpan Duration { get; set; }
    }
}