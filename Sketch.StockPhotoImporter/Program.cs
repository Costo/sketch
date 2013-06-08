using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mono.Options;
using Sketch.Core;
using Sketch.Core.Database;
using Sketch.Core.Entities;
using Sketch.StockPhotoImporter.Scraping;
using Sketch.StockPhotoImporter.Syndication;
using System.Data.Entity;

namespace Sketch.StockPhotoImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            Initialize();

            var importer = new Importer(() => new SketchDbContext());

            OptionSet p = new OptionSet()
              .Add("u=", url => importer.Url = url );
            try
            {
                p.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (importer.Url == null)
            {
                throw new InvalidOperationException();
            }

            importer.Start();
        }

        private static void Initialize()
        {
            var profile = new AutoMapperProfile();
            Mapper.AddProfile(profile);
            Mapper.AssertConfigurationIsValid(profile.ProfileName);

            Database.SetInitializer<SketchDbContext>(null);
            using (var context = new SketchDbContext())
            {
                if (!context.Database.Exists())
                {
                    ((IObjectContextAdapter) context).ObjectContext.CreateDatabase();
                }
            }
        }
    }

    internal class Importer
    {
        private readonly Func<DbContext> _contextFactory;

        public Importer(Func<DbContext> contextFactory )
        {
            _contextFactory = contextFactory;
        }

        public string Url { get; set; }

        public void Start()
        {
            var scraper = new Scraper(Url);
            var rssFeedUrl = scraper.GetRssFeedUrl();
            if (rssFeedUrl != null)
            {
                var feed = new Feed(rssFeedUrl);
                using (var context = _contextFactory.Invoke())
                {
                    var set = context.Set<StockPhoto>();
                    foreach (var item in feed.Where(x => x.HasContent))
                    {
                        var photo = Mapper.Map<StockPhoto>(item);
                        
                        var existing = set.Find(photo.ImageUrl);
                        if (existing == null) set.Add(photo);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}
