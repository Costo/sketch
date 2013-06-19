using Microsoft.Practices.Unity;
using Sketch.Core.Infrastructure;

namespace Sketch.EventStoreUtility
{
    public class Module
    {
        public void Init(IUnityContainer container)
        {
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
