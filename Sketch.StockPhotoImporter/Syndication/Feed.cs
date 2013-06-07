using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sketch.StockPhotoImporter.Syndication
{
    public class Feed: IEnumerable<FeedItem>
    {
        readonly XDocument document;

        public Feed(XDocument document)
        {
            this.document = document;
        }

        public Feed(string feedUrl)
        {
            this.document = XDocument.Load(feedUrl);
        }

        IEnumerator<FeedItem> IEnumerable<FeedItem>.GetEnumerator()
        {
            return document.Descendants("item")
                           .Select(FeedItem.Create).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<FeedItem>)this).GetEnumerator();
        }
    }
}