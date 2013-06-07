using HtmlAgilityPack;

namespace Sketch.Web.Scraping
{
    public class Scraper
    {
        readonly HtmlDocument _document;
        public Scraper(HtmlDocument document)
        {
            this._document = document;
        }

        public string GetRssFeedUrl()
        {
            var navigator = _document.CreateNavigator();
            var node = navigator.SelectSingleNode("//link[@rel='alternate'][@type='application/rss+xml']");
            if (node == null) return null;
            return node.GetAttribute("href","");
        }
    }
}