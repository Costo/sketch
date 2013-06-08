using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using Sketch.Core.ReadModel;
using Sketch.Web.Models;

namespace Sketch.Web.Controllers
{
    public class HomeController : Controller
    {
        readonly IStockPhotoDao _dao;

        public HomeController(IStockPhotoDao dao)
        {
            _dao = dao;
        }

        public ActionResult Index()
        {
            var photos = _dao.GetRandomStockPhotos(10);
            return View(new DrawingSessionModel
            {
                Photos = Mapper.Map<DrawingSessionModel.TimedPhoto[]>(photos)
            });
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
