using System.Data.Entity;
using Sketch.Core.ReadModel;

namespace Sketch.Core.Database
{
    public class SketchDbContext: DbContext
    {
        public SketchDbContext()
            : base("Sketch")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DrawingSessionDetail>().HasMany(e => e.Photos)
                .WithRequired(o => o.DrawingSession)
                .HasForeignKey(x=>x.DrawingSessionId)
                .WillCascadeOnDelete();

        }

        public DbSet<StockPhotoDetail> StockPhotos { get; set; }
        public DbSet<DrawingSessionDetail> DrawingSessions { get; set; }
        public DbSet<DrawingSessionPhoto> DrawingSessionPhotos { get; set; }
    }
}