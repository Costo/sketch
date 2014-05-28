using System;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Mono.Options;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;

namespace Sketch.EventStoreUtility
{
    class Program
    {
        private static IServiceLocator ServiceLocator;
        private static bool update;
        static void Main(string[] args)
        {
            Initialize();

            OptionSet p = new OptionSet()
              .Add("u", u => update = true);
            try
            {
                p.Parse(args);
            }
            catch (OptionException ex)
            {
                Console.WriteLine(ex.Message);
            }

            if (update)
            {
                var eventStore = new SqlEventStore(ServiceLocator.GetInstance<ITextSerializer>(), ServiceLocator.GetInstance<IEventBus>());
                eventStore.Advanced.ReplayEvents();
            }

            Console.ReadLine();
        }

        private static void Initialize()
        {
            var container = new UnityContainer();
            var unityServiceLocator = new UnityServiceLocator(container);
            Microsoft.Practices.ServiceLocation.ServiceLocator.SetLocatorProvider(() => unityServiceLocator);
            ServiceLocator = Microsoft.Practices.ServiceLocation.ServiceLocator.Current;
            new Core.Module().Init(container);
            new Module().Init(container);
        }

    }
}
