using System.Data.Entity;
using Sketch.Core.Entities;

namespace Sketch.Core.Database
{
    public class SketchDbContext: DbContext
    {
        public SketchDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<StockPhoto> StockPhotos { get; set; }
    }
}