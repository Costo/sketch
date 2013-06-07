using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mono.Options;
using Sketch.StockPhotoImporter.Scraping;
using Sketch.StockPhotoImporter.Syndication;

namespace Sketch.StockPhotoImporter
{
    class Program
    {
        static void Main(string[] args)
        {
            var importer = new Importer();

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
    }

    internal class Importer
    {
        public string Url { get; set; }

        public void Start()
        {
            var scraper = new Scraper(Url);
            var rssFeedUrl = scraper.GetRssFeedUrl();
            if (rssFeedUrl != null)
            {
                var feed = new Feed(rssFeedUrl);

                foreach (var item in feed)
                {
                    Console.WriteLine(item.Content);
                }
            }
        }
    }
}
