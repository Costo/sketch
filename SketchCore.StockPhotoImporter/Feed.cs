using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace SketchCore.StockPhotoImporter
{
    public class Feed : IEnumerable<FeedItem>
    {
        readonly XDocument document;

        public Feed(XDocument document)
        {
            this.document = document;
        }

        public string Next
        {
            get
            {
                return document
                    .Descendants("{http://www.w3.org/2005/Atom}link")
                    .Where(x => x.Attribute("rel").Value == "next")
                    .Select(x => x.Attribute("href").Value)
                    .FirstOrDefault();
            }
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
