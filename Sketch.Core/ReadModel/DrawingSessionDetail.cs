using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sketch.Core.ReadModel
{
    [Table("DrawingSessionDetail")]
    public class DrawingSessionDetail
    {
        [Key]
        public Guid Id { get; set; }
        public virtual IList<DrawingSessionPhoto> Photos { get; set; }

    }

    [Table("DrawingSessionPhoto")]
    public class DrawingSessionPhoto
    {
        [Key, Column(Order = 1)]
        public Guid DrawingSessionId { get; set; }
        public DrawingSessionDetail DrawingSession { get; set; }

        [Key, Column(Order = 2)]
        public string ImageUrl { get; set; }
        public TimeSpan Duration { get; set; }
        public int Order { get; set; }
    }
}