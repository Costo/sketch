using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Events
{
    public class StockPhotoCreated : DomainEvent
    {
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string PageUrl { get; set; }
        public string PublishedDate { get; set; }
        public DateTime ImportedDate { get; set; }
    }
}