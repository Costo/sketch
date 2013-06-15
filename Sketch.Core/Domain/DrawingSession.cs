using System;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Domain
{
    public partial class DrawingSession: AggregateRoot
    {
        public DrawingSession(Guid id):base(id)
        {
            this.Handles<DrawingSessionCreated>(NoOp);
            this.Handles<DrawingSessionPhotoAdded>(NoOp);

            this.Update(new DrawingSessionCreated());
        }

        public void AddPhoto(string photoUrl, TimeSpan duration)
        {
            this.Update(new DrawingSessionPhotoAdded
            {
                ImageUrl = photoUrl,
                Duration = duration
            });
        }
    }
}