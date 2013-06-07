using System;
using HtmlAgilityPack;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketch.StockPhotoImporter.Scraping;

namespace Sketch.Test
{
    [TestClass]
    public class ScraperFixture
    {
        private const string RssFeedUrl =
            "http://backend.deviantart.com/rss.xml?q=gallery%3Asenshistock%2F41010&type=deviation";
        
        [TestMethod]
        public void can_get_rss_feed_from_page()
        {
            var document = new HtmlDocument();
            document.Load(GetType().Assembly.GetManifestResourceStream("Sketch.Test.sample-deviant-art-page.html"));

            var parser = new Scraper(document);
            var actual = parser.GetRssFeedUrl();

            Assert.AreEqual(RssFeedUrl, actual);
        }
        [TestMethod]
        public void when_html_document_doesnt_contain_rss_feed()
        {
            var document = new HtmlDocument();
            document.CreateElement("html");

            var parser = new Scraper(document);
            var actual = parser.GetRssFeedUrl();

            Assert.IsNull(actual);
        }
    }
}
