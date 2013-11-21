using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class StockPhotoCreated : DomainEvent
    {
        public string UniqueId { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
        public string PageUrl { get; set; }
        public string PublishedDate { get; set; }
        public DateTime ImportedDate { get; set; }
    }
}