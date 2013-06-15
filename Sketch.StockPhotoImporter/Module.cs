using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Sketch.Core.CommandHandlers;
using Sketch.Core.Database;
using Sketch.Core.Infrastructure;
using Sketch.Core.Infrastructure.Storage;
using Sketch.Core.ReadModel;
using Sketch.Core.ReadModel.Impl;

namespace Sketch.StockPhotoImporter
{
    public class Module
    {
        public void Init(IUnityContainer container)
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
