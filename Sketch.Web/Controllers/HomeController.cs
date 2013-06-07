using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using AutoMapper;
using HtmlAgilityPack;
using Sketch.Web.Models;
using Sketch.Web.Scraping;
using Sketch.Web.Syndication;

namespace Sketch.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repository = new StockPhotoRepository();
            var photos = repository.GetRandomStockPhotos(10);
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

    public class StockPhotoRepository

    {
        public StockPhoto[] GetRandomStockPhotos(int count)
        {
            var web = new HtmlWeb();
            var document = web.Load("http://browse.deviantart.com/resources/stockart/model/");
            var parser = new Scraper(document);
            var feedUrl = parser.GetRssFeedUrl();
            var feed = new Feed(XDocument.Load(feedUrl));

            var random = new Random();
            return feed
                .Where(x=>x.HasContent)
                .OrderBy(x=> random.NextDouble())
                .Take(count)
                .Select(StockPhoto.CreateFrom)
                .ToArray();
        }
    }

    public class StockPhoto
    {
        public static StockPhoto CreateFrom(FeedItem feedItem)
        {
            return new StockPhoto
            {
                ImageUrl = feedItem.Content
            };
        }

        public string ImageUrl
        {
            get; set;
        }
    }
}
