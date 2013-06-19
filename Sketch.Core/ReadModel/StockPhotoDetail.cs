using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sketch.Core.ReadModel
{
    [Table("StockPhotoDetail", Schema = "Sketch")]
    public class StockPhotoDetail
    {
        [Key]
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string PageUrl { get; set; }
        public string PublishedDate { get; set; }
        public DateTime ImportedDate { get; set; }
    }
}