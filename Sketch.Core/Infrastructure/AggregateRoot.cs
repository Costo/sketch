using System;
using System.Collections.Generic;
using Sketch.Core.CommandHandlers;

namespace Sketch.Core.Infrastructure
{
    public abstract class AggregateRoot : IEventSourced
    {
        private readonly IList<IEvent> _events = new List<IEvent>();
        private int _version = -1;
        private IDictionary<Type, Action<IEvent>> _handlers = new Dictionary<Type, Action<IEvent>>();

        protected AggregateRoot(Guid id)
        {
            this.Id = id;
        }

        protected Guid Id { get; set; }

        public IList<IEvent> Events {
            get { return this._events; }
        }

        protected void NoOp<T>(T @event) where T:IEvent
        {
        }

        protected void Handles<T>(Action<T> action) where T: IEvent
        {
            this._handlers.Add(typeof (T), x => action((T)x) );
        }

        protected void Update(IEvent @event)
        {
            @event.SourceId = this.Id;
            @event.Version = _version + 1;
            _handlers[@event.GetType()].Invoke(@event);
            _version = @event.Version;
            this.Events.Add(@event);
        }
    }
}