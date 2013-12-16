using System;
using System.Collections.Generic;
using System.Linq;
using Sketch.Core.Commands;
using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using AutoMapper;

namespace Sketch.Core.CommandHandlers
{
    public class DrawingSessionCommandHandler:
        ICommandHandler<GenerateDrawingSession>,
        ICommandHandler<ReplaceDrawingSessionPhoto>
    {
        readonly IEventStore _store;
        readonly IStockPhotoDao _stockPhotoDao;
        readonly IDrawingSessionDao _drawingSessionDao;
        readonly double[] timesInMinutes = new[] { 10, 5, 5, 2, 2, 2, 1, .75, .75, .75, .75 };


        public DrawingSessionCommandHandler(IEventStore store, IStockPhotoDao stockPhotoDao, IDrawingSessionDao drawingSessionDao)
        {
            _store = store;
            _stockPhotoDao = stockPhotoDao;
            _drawingSessionDao = drawingSessionDao;
        }

        public void Handle(GenerateDrawingSession command)
        {
            var drawingSession = new DrawingSession(command.DrawingSessionId);

            var photos = _stockPhotoDao.GetRandomStockPhotos(timesInMinutes.Length);
            var timeSpans = new Stack<TimeSpan>(timesInMinutes.Select(TimeSpan.FromMinutes));
            foreach (var p in photos)
            {
                drawingSession.AddPhoto(p.StockPhotoId, p.PageUrl, Mapper.Map<OEmbedInfo>(p.OEmbed), timeSpans.Pop());
            }

            _store.Save(drawingSession, command.Id.ToString());
        }

        public void Handle(ReplaceDrawingSessionPhoto command)
        {
            var drawingSession = _store.Get<DrawingSession>(command.DrawingSessionId);

            var randomPhotos = _stockPhotoDao.GetRandomStockPhotos(12);
            var drawingSessionPhotoIds = _drawingSessionDao
                .Find(command.DrawingSessionId)
                .Photos
                .Select(x=>x.StockPhotoId)
                .ToArray();

            var photo = randomPhotos
                .Where(p => !drawingSessionPhotoIds.Contains(p.StockPhotoId))
                .First();

            drawingSession.ReplacePhoto(command.IndexOfPhotoToReplace, photo.StockPhotoId, photo.PageUrl, Mapper.Map<OEmbedInfo>(photo.OEmbed));

            _store.Save(drawingSession, command.Id.ToString());
        }
    }
}
