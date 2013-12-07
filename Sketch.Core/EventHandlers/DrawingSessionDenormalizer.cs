using System;
using System.Data.Entity;
using System.Linq;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using AutoMapper;

namespace Sketch.Core.EventHandlers
{
    public class DrawingSessionDenormalizer: 
        IEventHandler<DrawingSessionCreated>,
        IEventHandler<DrawingSessionPhotoAdded>,
        IEventHandler<DrawingSessionPhotoReplaced>
    {
        readonly Func<DbContext> _contextFactory;

        public DrawingSessionDenormalizer(Func<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public void Handle(DrawingSessionCreated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var drawingSession = new DrawingSessionDetail
                {
                    Id = @event.SourceId
                };
                context.Set<DrawingSessionDetail>().Add(drawingSession);
                context.SaveChanges();
            } 
        }

        public void Handle(DrawingSessionPhotoAdded @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var drawingSession = context.Set<DrawingSessionDetail>().Find(@event.SourceId);
                drawingSession.Photos.Add(new DrawingSessionPhoto
                {
                    DrawingSessionId = @event.SourceId,
                    StockPhotoId = @event.StockPhotoId,
                    Duration = @event.Duration,
                    PageUrl = @event.PageUrl,
                    OEmbed = Mapper.Map<OEmbedInfoDetail>(@event.OEmbed),
                    Order = @event.Index,
                });

                context.SaveChanges();
            }
        }

        public void Handle(DrawingSessionPhotoReplaced @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var drawingSession = context.Set<DrawingSessionDetail>().Find(@event.SourceId);

                var photo = drawingSession.Photos.Single(x => x.Order == @event.IndexOfPhoto);
                drawingSession.Photos.Remove(photo);
                drawingSession.Photos.Add(new DrawingSessionPhoto
                {
                    DrawingSessionId = @event.SourceId,
                    StockPhotoId = @event.NewStockPhotoId,
                    PageUrl = @event.NewPageUrl,
                    OEmbed = Mapper.Map<OEmbedInfoDetail>(@event.OEmbed),
                    Duration = photo.Duration,
                    Order = photo.Order,
                });

                context.SaveChanges();
            }
        }
    }
}
