using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;
using Unity.Mvc4;

namespace Sketch.Web
{
    public static class Bootstrapper
    {
        public static IUnityContainer Initialize()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));

            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            new Sketch.Core.Module().Init(container);
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<ITextSerializer, JsonSerializer>();
            var eventBus = new InMemoryEventBus();
            container.RegisterInstance<IEventBus>(eventBus);
            var commandBus = new InMemoryCommandBus();
            container.RegisterInstance<ICommandBus>(commandBus);

            container.RegisterType<IEventStore, SqlEventStore>();

            foreach (var handler in container.ResolveAll<ICommandHandler>())
            {
                commandBus.Register(handler);
            }

            foreach (var handler in container.ResolveAll<IEventHandler>())
            {
                eventBus.Register(handler);
            }
        }
    }
}