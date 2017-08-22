using System;
using System.Collections.Generic;
using System.Text;

namespace SketchCore.Core.Models
{
    public class StockPhoto
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
        public string PageUrl { get; set; }
        public string PublishedDate { get; set; }
        public DateTime ImportedDate { get; set; }

        public OEmbedInfo OEmbedInfo { get; set; }
    }
}
