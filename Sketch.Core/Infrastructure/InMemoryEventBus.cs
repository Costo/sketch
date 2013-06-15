using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sketch.Core.Infrastructure.Storage;

namespace Sketch.Core.Infrastructure
{
    public class InMemoryEventBus: IEventBus, IEventHandlerRegistry
    {
        readonly IList<IEventHandler> _handlers = new List<IEventHandler>();

        public void Publish(IEvent @event)
        {
            var handlerType = typeof (IEventHandler<>).MakeGenericType(@event.GetType());
            var handlers = this._handlers.Where(handlerType.IsInstanceOfType)
                .ToArray();
            foreach (var handler in handlers)
            {
                handlerType.InvokeMember("Handle",
                                         BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod,
                                         null,
                                         handler,
                                         new[] {@event});
            }
        }

        public void Publish(IEnumerable<IEvent> events) 
        {
            foreach (var @event in events)
            {
                Publish(@event);
            }
        }

        public void Register<T>(T handler) where T : IEventHandler
        {
            this._handlers.Add(handler);
        }
    }
}
