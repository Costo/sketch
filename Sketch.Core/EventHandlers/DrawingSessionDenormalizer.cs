﻿using System;
using System.Data.Entity;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;

namespace Sketch.Core.EventHandlers
{
    public class DrawingSessionDenormalizer: 
        IEventHandler<DrawingSessionCreated>,
        IEventHandler<DrawingSessionPhotoAdded>
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
                    Duration = @event.Duration,
                    ImageUrl = @event.ImageUrl,
                    Order = @event.Order, 
                });

                context.SaveChanges();
            }
        }
    }
}