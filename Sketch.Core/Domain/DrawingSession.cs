using System;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Domain
{
    public class DrawingSession: AggregateRoot
    {
        private int photoCount = 0;
        public DrawingSession(Guid id):base(id)
        {
            this.Handles<DrawingSessionCreated>(NoOp);
            this.Handles<DrawingSessionPhotoAdded>(OnDrawingSessionPhotoAdded);

            this.Update(new DrawingSessionCreated());
        }

        public void AddPhoto(string photoUrl, TimeSpan duration)
        {
            this.Update(new DrawingSessionPhotoAdded
            {
                ImageUrl = photoUrl,
                Duration = duration,
                Order = photoCount,
            });
        }

        void OnDrawingSessionPhotoAdded(DrawingSessionPhotoAdded @event)
        {
            photoCount++;
        }
    }
}