using System;
using System.Linq;
using System.Web.Mvc;
using Sketch.Core.Commands;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using Sketch.Web.Models;

namespace Sketch.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandBus _commandBus;
        readonly IDrawingSessionDao _dao;

        public HomeController(ICommandBus commandBus, IDrawingSessionDao dao)
        {
            _commandBus = commandBus;
            _dao = dao;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormCollection form)
        {
            var drawingSessionId = Guid.NewGuid();
            _commandBus.Send(new GenerateDrawingSession
            {
                DrawingSessionId = drawingSessionId
            });

            return RedirectToAction("Draw", new { Id = drawingSessionId });
        }

        public ActionResult Draw(Guid id, int? index)
        {
            var session = _dao.Find(id);
            if (session == null) return HttpNotFound();

            var photo = session.Photos.SingleOrDefault(x => x.Order == index.GetValueOrDefault());
            if (photo == null) return HttpNotFound();



            return View(new DrawingSessionPhotoViewModel
                            {
                                DrawingSessionId = id,
                                DurationInMilliseconds = (int)photo.Duration.TotalMilliseconds,
                                ImageUrl = photo.ImageUrl,
                                NumberOfElapsedPhotos = index.GetValueOrDefault(),
                                NumberOfRemaningPhotos = session.Photos.Count - index.GetValueOrDefault(),
                                NextPageUrl = Url.Action("Draw", new
                                                                     {
                                                                         id,
                                                                         index= ++index
                                                                     })
                            });
        }


        public ActionResult Replace(Guid id, int index)
        {
            _commandBus.Send(new ReplaceDrawingSessionPhoto
            {
                DrawingSessionId = id,
                IndexOfPhotoToReplace = index,
            });

            return RedirectToAction("Draw", new { Id = id, index = index });
        }
    }
}
