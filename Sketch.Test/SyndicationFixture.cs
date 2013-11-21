using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketch.StockPhotoImporter.Syndication;

namespace Sketch.Test
{
    [TestClass]
    public class SyndicationFixture
    {
        [TestMethod]
        public void can_extract_image_urls_from_rss_feed()
        {
            XDocument document;
            using (var stream = GetType().Assembly.GetManifestResourceStream("Sketch.Test.sample-deviant-art-rss.xml"))
            {
                document = XDocument.Load(stream);
            }
            var images = document.Root.Descendants(XName.Get("content", "http://search.yahoo.com/mrss/"));

                Assert.AreNotEqual(0, images.Count());
                var firstUrl = images.First().Attribute("url");
                Assert.IsNotNull(firstUrl);
            

        }

        [TestMethod]
        public void feed_enumerate_feed_items()
        {
            XDocument document;
            using (var stream = GetType().Assembly.GetManifestResourceStream("Sketch.Test.sample-deviant-art-rss.xml"))
            {
                document = XDocument.Load(stream);
            }
            var feed = new Feed(document);

            Assert.IsTrue(feed is IEnumerable<FeedItem>);
            Assert.AreNotEqual(0, feed.Count());
        }

        [TestMethod]
        public void feed_item_is_created_from_xelement()
        {
            XDocument document;
            var xmlnsMedia = XNamespace.Get("http://search.yahoo.com/mrss/");
            using (var stream = GetType().Assembly.GetManifestResourceStream("Sketch.Test.sample-deviant-art-rss.xml"))
            {
                document = XDocument.Load(stream);
            }
            var expected = document.Root
                .Descendants("item")
                .First();

            var actual =  FeedItem.Create(expected);

            Assert.AreEqual((string)expected.Element("title"), actual.Title);
            Assert.AreEqual((string)expected.Element("link"), actual.Link);
            Assert.AreEqual((string)expected.Element("pubDate"), actual.PubDate);
            Assert.AreEqual((string)expected.Element(xmlnsMedia.GetName("description")), actual.Description);
            Assert.AreEqual((string)expected.Element(xmlnsMedia.GetName("content")).Attribute("url"), actual.Content);
            Assert.AreEqual((string)expected.Element(xmlnsMedia.GetName("rating")), actual.Rating);
            Assert.AreEqual((string)expected.Element(xmlnsMedia.GetName("category")), actual.Category);

        }
    }
}
