﻿using System;
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
                .FromSql($"select top ({count}) * from StockPhoto where IsDeleted = 0 order by newid()")
                .AsNoTracking()
                .ToArray();

            var durations = new[] { 30, 30, 30, 60, 60, 120, 120, 5 * 60, 5 * 60, 10 * 60 };
            var routeValues = BuildRouteValues(
                photos[0].Id,
                photos.Select(x => x.Id).ToArray(),
                durations
                );
            return RedirectToAction(nameof(Draw), routeValues);
        }

        public async Task<IActionResult> Draw(
            int id,
            [FromForm]string operation,
            [FromQuery(Name = "p")]int[] photos,
            [FromQuery(Name = "d")]int[] durations)
        {
            var indexOfPhoto = Array.IndexOf(photos, id);

            if (Request.Method == "POST")
            {
                var photo = await _dbContext.Set<StockPhoto>().FindAsync(id);
                if(operation == "ban")
                {
                    photo.IsDeleted = true;
                    await _dbContext.SaveChangesAsync();
                }
                
                var idParams = string.Join(',', photos.Select((_, index) => $"{{{index}}}"));
                var replacement = _dbContext
                    .Set<StockPhoto>()
                    .FromSql($"select top 1 * from StockPhoto where IsDeleted = 0 and Id not in ({idParams}) order by newid()", photos.Cast<object>().ToArray())
                    .AsNoTracking()
                    .First();

                id = replacement.Id;
                photos[indexOfPhoto] = id;

                return RedirectToAction(nameof(Draw), BuildRouteValues(id, photos, durations));
            }
            else
            {
                var photo = await _dbContext.Set<StockPhoto>().FindAsync(id);
                var duration = durations[indexOfPhoto];
                var nextId = photos[indexOfPhoto + 1];

                return View(new DrawViewModel
                {
                    ImageUrl = photo.ContentUrl,
                    PhotoCount = photos.Length,
                    PhotoIndex = indexOfPhoto,
                    Next = Url.Action(nameof(Draw), BuildRouteValues(nextId, photos, durations)),
                    Duration = TimeSpan.FromSeconds(duration)
                });
            }
        }

        private object BuildRouteValues(int id, int[] photos, int[] durations)
        {
            return new
            {
                id,
                p = new StringValues(photos.Select(x => x.ToString()).ToArray()),
                d = new StringValues(durations.Select(x => x.ToString()).ToArray())
            };
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
