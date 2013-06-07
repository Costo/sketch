using System.Xml.Linq;

namespace Sketch.Web.Syndication
{
    public class FeedItem
    {
        public static FeedItem Create(XElement element)
        {
            var xmlnsMedia = XNamespace.Get("http://search.yahoo.com/mrss/");
            var contentElement = element.Element(xmlnsMedia.GetName("content"));
            return new FeedItem
                       {
                           Title = (string)element.Element("title"),
                           Link = (string)element.Element("link"),
                           PubDate = (string)element.Element("pubDate"),
                           Description = (string)element.Element(xmlnsMedia.GetName("description")),
                           Content = contentElement == null 
                               ? default(string) 
                               : (string)contentElement.Attribute("url"),
                       };
        }

        public string Title { get; private set; }

        public string Link { get; private set; }

        public string PubDate { get; private set; }

        public string Description { get; private set; }

        public string Content { get; private set; }

        public bool HasContent
        {
            get { return !string.IsNullOrEmpty(Content); }
        }
    }
}