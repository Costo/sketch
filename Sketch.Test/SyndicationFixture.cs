using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sketch.Web.Syndication;

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
            
            Assert.AreEqual(expected.Element("title").Value, actual.Title);
            Assert.AreEqual(expected.Element("link").Value, actual.Link);
            Assert.AreEqual(expected.Element("pubDate").Value, actual.PubDate);
            Assert.AreEqual(expected.Element(xmlnsMedia.GetName("description")).Value, actual.Description);
            Assert.AreEqual((string)expected.Element(xmlnsMedia.GetName("content")).Attribute("url"), actual.Content);

        }
    }
}
