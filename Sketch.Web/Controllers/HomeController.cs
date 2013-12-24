﻿using System;
using System.Linq;
using System.Web.Mvc;
using Sketch.Core.Commands;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using Sketch.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;

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

        public ActionResult Draw(Guid id, int index=0)
        {
            var session = _dao.Find(id);
            if (session == null) return HttpNotFound();

            var photo = session.Photos.SingleOrDefault(x => x.Order == index);
            if (photo == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);


            var remainingPhotos = session.Photos.Count - (index+1);
            string nextPageUrl = Url.Action("Draw", new
                                                    {
                                                        id,
                                                        index = index+1
                                                    });
            if(remainingPhotos == 0)
            {
                nextPageUrl = Url.Action("Summary", new { id });
            }

            return View(new DrawingSessionPhotoViewModel
                            {
                                DrawingSessionId = id,
                                DurationInMilliseconds = (int)photo.Duration.TotalMilliseconds,
                                ImageUrl = photo.OEmbed.Url,
                                NumberOfElapsedPhotos = index,
                                NumberOfRemaningPhotos = session.Photos.Count - index,
                                NextPageUrl = nextPageUrl
                            });
        }


        [HttpPost]
        public ActionResult Replace(Guid id, int index)
        {
            _commandBus.Send(new ReplaceDrawingSessionPhoto
            {
                DrawingSessionId = id,
                IndexOfPhotoToReplace = index,
            });

            return RedirectToAction("Draw", new { Id = id, index = index });
        }

        [HttpPost]
        public ActionResult Ban(Guid id, int index)
        {
            var session = _dao.Find(id);
            if (session == null) return HttpNotFound();

            var photo = session.Photos.SingleOrDefault(x => x.Order == index);
            if (photo == null) return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);

            _commandBus.Send(new ReplaceDrawingSessionPhoto
            {
                DrawingSessionId = id,
                IndexOfPhotoToReplace = index,
            });

            _commandBus.Send(new BanStockPhoto
            {
                StockPhotoId = photo.StockPhotoId,
            });

            return RedirectToAction("Draw", new { Id = id, index = index });
        }

        public ActionResult Summary(Guid id)
        {
            var session = _dao.Find(id);
            if (session == null) return HttpNotFound();

            return View(session);
        }
    }
}
