using SketchCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SketchCore.Functions
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
                Copyright = (string)element.Element(xmlnsMedia + "copyright"),
                Content = contentElement == null
                               ? default(Image)
                               : Image.Create(contentElement),
                Thumbnails = element.Elements(xmlnsMedia + "thumbnail")
                                    .Select(Image.Create)
                                    .ToArray()
            };
        }

        public string Guid { get; private set; }
        public string Title { get; private set; }

        public string Link { get; private set; }

        public string PubDate { get; private set; }

        public string Description { get; private set; }

        public IList<Image> Thumbnails { get; private set; }
        public Image Content { get; private set; }

        public string Rating { get; set; }

        public string Category { get; set; }

        public string Copyright { get; private set; }

        public bool HasContent => Content != null;

        public class Image
        {
            public int Width { get; set; }
            public int Height { get; set; }
            public string Url { get; set; }

            public static Image Create(XElement element)
            {
                return new Image
                {
                    Url = (string)element.Attribute("url"),
                    Width = (int)(double)element.Attribute("width"),
                    Height = (int)(double)element.Attribute("height")
                };
            }
        }
    }
}
