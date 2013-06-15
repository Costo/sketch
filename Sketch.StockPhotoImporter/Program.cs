using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Mono.Options;
using Sketch.Core;
using Sketch.Core.Commands;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;
using Sketch.Core.ReadModel;
using Sketch.StockPhotoImporter.Scraping;
using Sketch.StockPhotoImporter.Syndication;
using System.Data.Entity;

namespace Sketch.StockPhotoImporter
{
    class Program
    {
        private static IServiceLocator ServiceLocator;
        static void Main(string[] args)
        {
            Initialize();

            var importer = new Importer(ServiceLocator.GetInstance<ICommandBus>(), ServiceLocator.GetInstance<IStockPhotoDao>(), new Clock());

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
            Database.SetInitializer<EventStoreDbContext>(null);
            using (var sketchDbContext = new SketchDbContext())
            using (var eventStoreDbContext = new EventStoreDbContext())
            {
                if (!sketchDbContext.Database.Exists())
                {
                    ((IObjectContextAdapter)sketchDbContext).ObjectContext.CreateDatabase();

                    eventStoreDbContext.Database.ExecuteSqlCommand(((IObjectContextAdapter)eventStoreDbContext).ObjectContext.CreateDatabaseScript());
                }
            }

            var container = new UnityContainer();
            var unityServiceLocator = new UnityServiceLocator(container);
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            ServiceLocator = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;
            new Core.Module().Init(container);
            new Module().Init(container);
        }
    }

    internal class Importer
    {
        readonly ICommandBus _commandBus;
        readonly IStockPhotoDao _dao;
        readonly IClock _clock;

        public Importer(ICommandBus commandBus, IStockPhotoDao dao, IClock clock )
        {
            _commandBus = commandBus;
            _dao = dao;
            _clock = clock;
        }

        public string Url { get; set; }

        public void Start()
        {
            var scraper = new Scraper(Url);
            var rssFeedUrl = scraper.GetRssFeedUrl();
            if (rssFeedUrl != null)
            {
                var feed = new Feed(rssFeedUrl);
                foreach (var item in feed.Where(x => x.HasContent))
                {
                    if (!_dao.Exists(imageUrl: item.Content))
                    {
                        var command = Mapper.Map<ImportStockPhoto>(item);
                        command.StockPhotoId = Guid.NewGuid();
                        _commandBus.Send(command);
                    }
                }
            }
        }
    }
}
