using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Sketch.Web.Syndication
{
    public class Feed: IEnumerable<FeedItem>
    {
        private XDocument document;

        public Feed(XDocument document)
        {
            this.document = document;
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