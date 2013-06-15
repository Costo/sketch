using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sketch.Core.ReadModel
{
    [Table("DrawingSessionDetail")]
    public class DrawingSessionDetail
    {
        [Key]
        public Guid Id { get; set; }

    }
}