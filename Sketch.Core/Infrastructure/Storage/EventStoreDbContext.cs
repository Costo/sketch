using System.Data.Entity;

namespace Sketch.Core.Infrastructure.Storage
{
    public class EventStoreDbContext: DbContext
    {
        public EventStoreDbContext()
            : base("DefaultConnection")
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
