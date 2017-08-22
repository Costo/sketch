using SketchCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SketchCore.StockPhotoImporter
{
    public class FeedItem
    {
        public static FeedItem Create(XElement element)
        {
            var xmlnsMedia = XNamespace.Get("http://search.yahoo.com/mrss/");
            var contentElement = element.Element(xmlnsMedia.GetName("content"));
            return new FeedItem
            {
                Guid = (string)element.Element("guid"),
                Title = (string)element.Element("title"),
                Link = (string)element.Element("link"),
                PubDate = (string)element.Element("pubDate"),
                Description = (string)element.Element(xmlnsMedia.GetName("description")),
                Rating = (string)element.Element(xmlnsMedia.GetName("rating")),
                Category = (string)element.Element(xmlnsMedia.GetName("category")),
                Content = contentElement == null
                               ? default(string)
                               : (string)contentElement.Attribute("url"),
            };
        }

        public string Guid { get; private set; }
        public string Title { get; private set; }

        public string Link { get; private set; }

        public string PubDate { get; private set; }

        public string Description { get; private set; }

        public string Content { get; private set; }

        public string Rating { get; set; }

        public string Category { get; set; }

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(Content); }
        }

        public Task<OEmbedInfo> FetchOEmbedInfo()
        {
            throw new NotImplementedException();
        }

    }
}
