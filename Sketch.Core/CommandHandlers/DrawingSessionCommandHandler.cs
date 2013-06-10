using System;
using System.Linq;
using Sketch.Core.Commands;
using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;

namespace Sketch.Core.CommandHandlers
{
    public class DrawingSessionCommandHandler: ICommandHandler<GenerateDrawingSession>
    {
        private readonly IEventStore _store;
        private readonly IStockPhotoDao _dao;

        public DrawingSessionCommandHandler(IEventStore store, IStockPhotoDao dao)
        {
            _store = store;
            _dao = dao;
        }

        public void Handle(GenerateDrawingSession command)
        {
            var drawingSession = new DrawingSession(command.DrawingSessionId);

            var photos = _dao.GetRandomStockPhotos(10);
            foreach (var p in photos)
            {
                drawingSession.AddPhoto(p.ImageUrl, TimeSpan.FromSeconds(10));
            }

            _store.Save(drawingSession, command.Id.ToString());
        }
    }
}
