using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class DrawingSessionPhotoReplaced : DomainEvent
    {
        public string NewImageUrl { get; set; }
        public int IndexOfPhoto { get; set; }
    }
}