using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AutoMapper;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure.Storage;

namespace Sketch.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Bootstrapper.Initialize();

            var profile = new AutoMapperProfile();
            Mapper.AddProfile(profile);
            Mapper.AssertConfigurationIsValid(profile.ProfileName);

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

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
                
        }
    }
}