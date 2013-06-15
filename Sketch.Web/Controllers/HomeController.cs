using System;
using System.Web.Mvc;
using AutoMapper;
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

        public ActionResult Draw(Guid id)
        {
            var model = _dao.Find(id);

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
