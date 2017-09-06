using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SketchCore.Core.Models;

namespace SketchCore.Core.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<StockPhoto>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasAlternateKey(x => x.UniqueId);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.UniqueId).HasMaxLength(256);
                b.Property(x => x.Title).HasMaxLength(256);
                b.Property(x => x.Description);
                b.Property(x => x.Copyright).HasMaxLength(256);
                b.Property(x => x.Rating).HasMaxLength(256);
                b.Property(x => x.Category).HasMaxLength(256);
                b.Property(x => x.PageUrl).HasMaxLength(256);
                b.Property(x => x.ContentUrl).HasMaxLength(256);
                b.Property(x => x.PublishedDate).HasMaxLength(256);
                b.HasQueryFilter(p => !p.IsDeleted);
            });

            builder.Entity<Thumbnail>(b =>
            {
                b.HasKey(x => x.Id);
                b.HasOne(x => x.Photo).WithMany(x => x.Thumbnails).OnDelete(DeleteBehavior.Cascade);
                b.Property(x => x.Url).HasMaxLength(512);
            });

            
        }
    }
}
