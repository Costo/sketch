using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using SketchCore.Core.Data;
using SketchCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using SketchCore.Web.Models;

namespace SketchCore.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormCollection form)
        {
            const int count = 10;
            var photos = _dbContext
                .Set<StockPhoto>()
                .FromSql($"select top ({count}) * from StockPhoto order by newid()")
                .AsNoTracking()
                .ToArray();

            var durations = new[] { 30, 30, 30, 60, 60, 120, 120, 5 * 60, 5 * 60, 10 * 60 };
            var @params = new
            {
                id = photos[0].Id,
                p = new StringValues(photos.Select(x => x.Id.ToString()).ToArray()),
                d = new StringValues(durations.Select(x => x.ToString()).ToArray())
            };
            return RedirectToAction(nameof(Draw), @params);
        }

        public async Task<IActionResult> Draw(
            int id,
            [FromQuery(Name = "p")]int[] photos,
            [FromQuery(Name = "d")]int[] durations)
        {
            var photo = await _dbContext.Set<StockPhoto>().FindAsync(id);
            var duration = durations[Array.IndexOf(photos, id)];

            var @params = new
            {
                id = photos[Array.IndexOf(photos, id) + 1],
                p = new StringValues(photos.Select(x => x.ToString()).ToArray()),
                d = new StringValues(durations.Select(x => x.ToString()).ToArray())
            };
            return View(new DrawViewModel {
                ImageUrl = photo.ContentUrl,
                PhotoCount = photos.Length,
                PhotoIndex = Array.IndexOf(photos, id),
                Next = Url.Action(nameof(Draw), @params),
                Duration = TimeSpan.FromSeconds(duration)
            });
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
