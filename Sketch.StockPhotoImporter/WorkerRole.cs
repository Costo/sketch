using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using AutoMapper;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Microsoft.WindowsAzure.ServiceRuntime;
using Sketch.Core;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using Sketch.StockPhotoImporter;
using Module = Sketch.StockPhotoImporter.Module;

namespace Sketch.StockPhotoImporter
{
    public class WorkerRole : RoleEntryPoint
    {
        private static IServiceLocator ServiceLocator;
        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.WriteLine("WorkerRole entry point called", "Information");

            while (true)
            {

                var importer = new Importer(ServiceLocator.GetInstance<ICommandBus>(), ServiceLocator.GetInstance<IStockPhotoDao>(), new Clock());
                importer.Url = "http://browse.deviantart.com/resources/stockart/model/";
                importer.Start();

                Thread.Sleep(1000 * 3600);

            }
        }

        public override bool OnStart()
        {
            Trace.TraceInformation("Starting Worker Role");

            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.

            Database.DefaultConnectionFactory = new ConnectionFactory(Database.DefaultConnectionFactory);

            var profile = new AutoMapperProfile();
            Mapper.AddProfile(profile);
            Mapper.AssertConfigurationIsValid(profile.ProfileName);

            var container = new UnityContainer();
            var unityServiceLocator = new UnityServiceLocator(container);
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            ServiceLocator = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;
            new Sketch.Core.Module().Init(container);
            new Module().Init(container);

            return base.OnStart();
        }
    }
}
