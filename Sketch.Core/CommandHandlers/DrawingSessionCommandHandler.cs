﻿using System;
using System.Collections.Generic;
using System.Linq;
using Sketch.Core.Commands;
using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;

namespace Sketch.Core.CommandHandlers
{
    public class DrawingSessionCommandHandler: ICommandHandler<GenerateDrawingSession>
    {
        readonly IEventStore _store;
        readonly IStockPhotoDao _dao;
        readonly double[] timesInMinutes = new[] { 10, 5, 5, 2, 2, 2, 1, .75, .75, .75, .75 };


        public DrawingSessionCommandHandler(IEventStore store, IStockPhotoDao dao)
        {
            _store = store;
            _dao = dao;
        }

        public void Handle(GenerateDrawingSession command)
        {
            var drawingSession = new DrawingSession(command.DrawingSessionId);

            var photos = _dao.GetRandomStockPhotos(timesInMinutes.Length);
            var timeSpans = new Stack<TimeSpan>(timesInMinutes.Select(TimeSpan.FromMinutes));
            foreach (var p in photos)
            {
                drawingSession.AddPhoto(p.ImageUrl, timeSpans.Pop());
            }

            _store.Save(drawingSession, command.Id.ToString());
        }
    }
}
