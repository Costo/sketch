using Microsoft.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using System;
using SketchCore.Core.Data;
using System.IO;

namespace SketchCore.StockPhotoImporter
{
    class Program
    {
        private static IServiceProvider _runtimeServices;

        static void Main(string[] args)
        {
            var startupProjectDir = Directory.GetCurrentDirectory();

            var builder = new ConfigurationBuilder()
                .SetBasePath(startupProjectDir)
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.development.json", optional: true)
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var services = new ServiceCollection();
            ConfigureServices(services, configuration);
            _runtimeServices = services.BuildServiceProvider();

            var app = new CommandLineApplication();

            app.Command("import", config => {
                config.Description = "Import a DeviantArt page";
                var url = config.Argument("url", "url of the page", multipleValues: true);
                config.OnExecute(() => {
                    foreach(var value in url.Values)
                    {
                        new Importer(_runtimeServices).Import(value).Wait();
                    }
                    return 0;
                });
            });

            var result = app.Execute(args);
            Environment.Exit(result);
        }

        private static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }
    }
}