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

        public async Task<ActionResult> Draw(Guid id, int index=0)
        {
            var session = _dao.Find(id);
            if (session == null) return HttpNotFound();

            var photo = session.Photos.SingleOrDefault(x => x.Order == index);
            if (photo == null) return HttpNotFound();

            var remainingPhotos = session.Photos.Count - index;
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


        public ActionResult Replace(Guid id, int index)
        {
            _commandBus.Send(new ReplaceDrawingSessionPhoto
            {
                DrawingSessionId = id,
                IndexOfPhotoToReplace = index,
            });

            return RedirectToAction("Draw", new { Id = id, index = index });
        }

        public ActionResult Summary(Guid id)
        {
            return View();
        }
    }
}
