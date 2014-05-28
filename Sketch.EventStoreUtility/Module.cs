using Microsoft.Practices.Unity;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;
using System.Data.Entity;

namespace Sketch.EventStoreUtility
{
    public class Module
    {
        public void Init(IUnityContainer container)
        {
            Database.DefaultConnectionFactory = new ConnectionFactory(Database.DefaultConnectionFactory);
            Database.SetInitializer<SketchDbContext>(null);
            Database.SetInitializer<EventStoreDbContext>(null);

            container.RegisterType<ITextSerializer, JsonSerializer>();
            var eventBus = new InMemoryEventBus();
            container.RegisterInstance<IEventBus>(eventBus);

            foreach (var handler in container.ResolveAll<IEventHandler>())
            {
                eventBus.Register(handler);
            }

        }
    }
}
