using System;
using System.Collections.Generic;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Domain
{
    public class DrawingSession: AggregateRoot
    {
        private int photoCount = 0;

        public DrawingSession(Guid id, IEnumerable<IEvent> history) : this(id, creation: false)
        {
            LoadFrom(history);
        }

        public DrawingSession(Guid id, bool creation = true):base(id)
        {
            Handles<DrawingSessionCreated>(NoOp);
            Handles<DrawingSessionPhotoAdded>(OnDrawingSessionPhotoAdded);
            Handles<DrawingSessionPhotoReplaced>(NoOp);

            if (creation)
            {
                Update(new DrawingSessionCreated());
            }
        }

        public void AddPhoto(Guid stockPhotoId, string pageUrl, OEmbedInfo oEmbed, TimeSpan duration)
        {
            this.Update(new DrawingSessionPhotoAdded
            {
                StockPhotoId = stockPhotoId,
                PageUrl = pageUrl,
                OEmbed = oEmbed,
                Duration = duration,
                Index = photoCount,
            });
        }

        public void ReplacePhoto(int indexOfPhoto, Guid newStockPhotoId, string newPageUrl)
        {
            this.Update(new DrawingSessionPhotoReplaced
            {
                IndexOfPhoto = indexOfPhoto,
                NewStockPhotoId = newStockPhotoId,
                NewPageUrl = newPageUrl,
            });
        }

        void OnDrawingSessionPhotoAdded(DrawingSessionPhotoAdded @event)
        {
            photoCount++;
        }
    }
}