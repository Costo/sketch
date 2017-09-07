using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace SketchCore.Core.Models
{
    public class StockPhoto
    {
        public int Id { get; set; }
        public string UniqueId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Copyright { get; set; }
        public string Rating { get; set; }
        public string Category { get; set; }
        public string PageUrl { get; set; }
        public string ContentUrl { get; set; }
        public int ContentWidth { get; set; }
        public int ContentHeight { get; set; }
        public string PublishedDate { get; set; }
        public DateTime ImportedDate { get; set; }
        public IList<Thumbnail> Thumbnails { get; set; }
        public bool IsDeleted { get; set; }

        [NotMapped]
        public Thumbnail Thumbnail
        {
            get
            {
                return Thumbnails
                    ?.OrderBy(x => x.Width * x.Height)
                    .FirstOrDefault();
            }
        }
    }

    public class Thumbnail
    {
        public int Id { get; set; }
        public StockPhoto Photo { get; set; }
        public string Url { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
