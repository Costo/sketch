using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Sketch.Web.Controllers;

namespace Sketch.Web.Models
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