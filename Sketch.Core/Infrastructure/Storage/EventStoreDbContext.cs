using System.Data.Entity;

namespace Sketch.Core.Infrastructure.Storage
{
    public class EventStoreDbContext: DbContext
    {
        public EventStoreDbContext()
            : base("EventStore")
        {
        }

        public DbSet<Event> Events { get; set; }
    }
}
