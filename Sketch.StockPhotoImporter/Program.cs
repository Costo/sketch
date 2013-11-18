using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Mono.Options;
using Sketch.Core;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;
using Sketch.Core.ReadModel;
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

            Database.DefaultConnectionFactory = new ConnectionFactory(Database.DefaultConnectionFactory);
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
}
