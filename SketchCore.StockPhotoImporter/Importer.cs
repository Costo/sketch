using SketchCore.Core.Data;
using System;
using System.Collections.Generic;
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
        private HttpClient _httpClient = new HttpClient();

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
                        Rating = item.Rating,
                        Category = item.Category,
                        PageUrl = item.Link,
                        PublishedDate = item.PubDate,
                        OEmbedInfo = new OEmbedInfo(),
                        //OEmbedInfo = await FetchOEmbedInfo(item.Link)
                    };

                    var dbContext = _runtimeServices.GetRequiredService<ApplicationDbContext>();

                    dbContext.Set<StockPhoto>().Add(stockPhoto);
                    dbContext.SaveChanges();

                }
                Console.WriteLine($"Imported {counter} items");
                rssFeedUrl = feed.Next;
            }
        }

        public async Task<OEmbedInfo> FetchOEmbedInfo(string pageUrl)
        {
            var client = new HttpClient();
            var result = await client
                .GetStringAsync("http://backend.deviantart.com/oembed?format=xml&url=" + Uri.EscapeDataString(pageUrl));

            var root = XDocument.Parse(result).Root;
            XNamespace ns = "https://www.deviantart.com/difi/";

            var oEmbed = new OEmbedInfo();
            oEmbed.Version = (string)root.Element(ns + "version");
            oEmbed.Type = (string)root.Element(ns + "type");
            oEmbed.Title = (string)root.Element(ns + "title");
            oEmbed.Url = (string)root.Element(ns + "url");
            oEmbed.AuthorName = (string)root.Element(ns + "author_name");
            oEmbed.AuthorUrl = (string)root.Element(ns + "author_url");
            oEmbed.ProviderName = (string)root.Element(ns + "provider_name");
            oEmbed.ProviderUrl = (string)root.Element(ns + "provider_url");
            oEmbed.ThumbnailUrl = (string)root.Element(ns + "thumbnail_url");
            oEmbed.ThumbnailWidth = (int)root.Element(ns + "thumbnail_width");
            oEmbed.ThumbnailHeight = (int)root.Element(ns + "thumbnail_height");
            oEmbed.Width = (int)root.Element(ns + "width");
            oEmbed.Height = (int)root.Element(ns + "height");

            return oEmbed;

        }

    }
}
