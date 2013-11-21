using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Commands
{
    public  class ImportStockPhoto: Command
    {
        public Guid StockPhotoId { get; set; }
        public string UniqueId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
        public string PageUrl { get; set; }
        public string PublishedDate { get; set; }

    }
}