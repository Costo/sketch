using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.WindowsAzure.Storage.Queue;
using System.Linq;
using SketchCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using SketchCore.Core.Data;

namespace SketchCore.Functions
{
    public static class DeepImport
    {
        private static HttpClient _httpClient = new HttpClient();
        [FunctionName("DeepImport")]
        public static async Task Run([QueueTrigger("deepimport", Connection = "AzureWebJobsStorage")]string rssFeedUrl, [Queue("deepimport", Connection = "AzureWebJobsStorage")]CloudQueue queue, TraceWriter log)
        {
            log.Info($"Importing: {rssFeedUrl}");

            ApplicationHelper.Configure();

            var message = new HttpRequestMessage(HttpMethod.Get, rssFeedUrl);
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("text/xml"));
            message.Headers.UserAgent.Add(new ProductInfoHeaderValue("Mozilla", "5.0"));
            var response = await _httpClient.SendAsync(message);
            if (!response.IsSuccessStatusCode)
            {
                log.Error($"Error: Status Code: [{response.StatusCode}] Message: [{response.ReasonPhrase}]");
                response.EnsureSuccessStatusCode(); // throws
            }
            var content = await response.Content.ReadAsStringAsync();
            var xdoc = XDocument.Parse(content);
            var feed = new Feed(xdoc);

            int counter = 0;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(ConfigurationManager.AppSettings["DefaultConnection"]);

            using (var dbContext = new ApplicationDbContext(builder.Options))
            {
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

                    
                    var existing = await dbContext.Set<StockPhoto>().FirstOrDefaultAsync(x => x.UniqueId == stockPhoto.UniqueId);
                    if(existing == null)
                    {
                        dbContext.Set<StockPhoto>().Add(stockPhoto);
                    }
                }
                await dbContext.SaveChangesAsync();
            }
            log.Info($"Imported {counter} items");

            if (feed.Next != null)
            {
                log.Info($"Queuing {feed.Next}");
                queue.AddMessage(new CloudQueueMessage(feed.Next), initialVisibilityDelay: TimeSpan.FromHours(1));
            }
        }
    }
}
