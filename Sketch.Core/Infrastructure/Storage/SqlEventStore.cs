using System;
using System.Linq;
using System.Reflection;

namespace Sketch.Core.Infrastructure.Storage
{
    public class SqlEventStore : IEventStore, IAdvancedEventStore
    {
        readonly ITextSerializer _serializer;
        readonly IEventBus _eventBus;

        public SqlEventStore(ITextSerializer serializer, IEventBus eventBus)
        {
            _serializer = serializer;
            _eventBus = eventBus;
        }

        public void Save(IEventSourced eventSourced, string correlationId)
        {
            using (var context = new EventStoreDbContext ())
            {
                foreach (var @event in eventSourced.Events)
                {
                    context.Set<Event>().Add(new Event
                    {
                        AggregateId = @event.SourceId,
                        CorrelationId = correlationId,
                        Version = @event.Version,
                        EventType = @event.GetType().ToString(),
                        Payload = _serializer.Serialize(@event),
                    });
                }
                context.SaveChanges();
            }
            _eventBus.Publish(events: eventSourced.Events);
        }

        public IAdvancedEventStore Advanced
        {
            get { return this; }
        }

        void IAdvancedEventStore.ReplayEvents()
        {
            using (var context = new EventStoreDbContext())
            {
                foreach (var @event in context.Events.OrderBy(x=>x.AggregateId).ThenBy(x=>x.Version))
                {
                    var deserializeMethod = _serializer.GetType()
                        .GetMethod("Deserialize", BindingFlags.Instance | BindingFlags.Public)
                        .MakeGenericMethod(Type.GetType(@event.EventType));

                    var payload = (IEvent)deserializeMethod.Invoke(_serializer, new [] { @event.Payload });
                    _eventBus.Publish(payload);

                }
            }
        }
    }
}
