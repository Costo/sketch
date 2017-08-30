using SketchCore.Core.Data;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SketchCore.Core.Models;
using System.Net.Http;
using System.Xml.Linq;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace SketchCore.StockPhotoImporter
{
    class Importer
    {
        private IServiceProvider _runtimeServices;
        private static HttpClient _httpClient = new HttpClient();

        public Importer(IServiceProvider runtimeServices)
        {
            _runtimeServices = runtimeServices;
        }

        public async Task Import(string url)
        {
            int counter = 0;
            Console.WriteLine($"Starting import of {url}");
            var scraper = new Scraper(url);
            var rssFeedUrl = scraper.GetRssFeedUrl();
            while (rssFeedUrl != null)
            {
                Console.WriteLine($"Importing RSS feed {rssFeedUrl}");

                var message = new HttpRequestMessage(HttpMethod.Get, rssFeedUrl);
                message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
                message.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
                var response = await _httpClient.SendAsync(message);
                if(!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error: Status Code: [{response.StatusCode}] Message: [{response.ReasonPhrase}]");
                    break;
                }
                var content = await response.Content.ReadAsStringAsync();
                var xdoc = XDocument.Parse(content);
                var feed = new Feed(xdoc);
                foreach (var item in feed.Where(x => x.HasContent))
                {
                    counter += 1;
                    var stockPhoto = new StockPhoto
                    {
                        UniqueId = item.Guid,
                        Title = item.Title,
                        Description = item.Description,
                        Rating = item.Rating,
                        Category = item.Category,
                        Copyright = item.Copyright,
                        PageUrl = item.Link,
                        PublishedDate = item.PubDate,
                        ImportedDate = DateTime.UtcNow,
                        ContentUrl = item.Content.Url,
                        ContentWidth = item.Content.Width,
                        ContentHeight = item.Content.Height,
                        Thumbnails = item.Thumbnails.Select(x => new Thumbnail
                        {
                            Url = x.Url,
                            Width = x.Width,
                            Height = x.Height
                        }).ToArray()
                    };

                    var dbContext = _runtimeServices.GetRequiredService<ApplicationDbContext>();
                    dbContext.Set<StockPhoto>().Add(stockPhoto);
                    dbContext.SaveChanges();
                }
                Console.WriteLine($"Imported {counter} items");
                rssFeedUrl = feed.Next;
            }
        }
    }
}
