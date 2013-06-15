using System.Collections.Generic;

namespace Sketch.Core.Infrastructure.Storage
{
    public class SqlEventStore: IEventStore
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
    }

    public interface IEventBus
    {
        void Publish(IEvent @event);
        void Publish(IEnumerable<IEvent> events);
    }
}
