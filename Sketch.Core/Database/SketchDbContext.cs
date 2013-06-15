using System.Data.Entity;
using Sketch.Core.ReadModel;

namespace Sketch.Core.Database
{
    public class SketchDbContext: DbContext
    {
        public SketchDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<StockPhotoDetail> StockPhotos { get; set; }
        public DbSet<DrawingSessionDetail> DrawingSessions { get; set; }
    }
}