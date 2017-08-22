using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;

namespace SketchCore.StockPhotoImporter
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
            return Uri.UnescapeDataString(node.GetAttribute("href", "")).Replace("&amp;", "&");
        }
    }
}
