using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sketch.Core.Infrastructure.Storage;

namespace Sketch.Core.Infrastructure
{
    public class InMemoryEventBus: IEventBus, IEventHandlerRegistry
    {
        private object _gate = new object();
        private List<IEvent> events = new List<IEvent>();

        readonly IList<IEventHandler> _handlers = new List<IEventHandler>();

        private readonly IDictionary<Type, IEventHandler[]> _registry = new Dictionary<Type, IEventHandler[]>(); 

        public void Publish(IEvent @event)
        {
            this.events.Add(@event);

            var handlerType = typeof(IEventHandler<>).MakeGenericType(@event.GetType());

            foreach (dynamic handler in this._handlers
                .Where(x => handlerType.IsAssignableFrom(x.GetType())))
            {
                handler.Handle((dynamic)@event);
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
