using System;
using HtmlAgilityPack;

namespace Sketch.StockPhotoImporter.Scraping
{
    public class Scraper
    {
        readonly HtmlDocument _document;
        public Scraper(HtmlDocument document)
        {
            _document = document;
        }

        public Scraper(string url)
        {
            _document = new HtmlWeb().Load(url);
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