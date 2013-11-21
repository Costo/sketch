using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sketch.Core.ReadModel
{
    [Table("DrawingSessionDetail", Schema = "Sketch")]
    public class DrawingSessionDetail
    {
        [Key]
        public Guid Id { get; set; }
        public virtual IList<DrawingSessionPhoto> Photos { get; set; }

    }

    [Table("DrawingSessionPhoto", Schema = "Sketch")]
    public class DrawingSessionPhoto
    {
        [Key, Column(Order = 1)]
        public Guid DrawingSessionId { get; set; }
        public DrawingSessionDetail DrawingSession { get; set; }

        [Key, Column(Order = 2)]
        public Guid StockPhotoId { get; set; }
        public string PageUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
    }
}