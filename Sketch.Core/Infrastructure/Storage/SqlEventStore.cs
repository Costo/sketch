using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.Practices.ObjectBuilder2;

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

        public T Get<T>(Guid id) where T : IEventSourced
        {
            using (var context = new EventStoreDbContext())
            {
                var events = context.Set<Event>()
                                    .Where(x => x.AggregateId == id)
                                    .OrderBy(x => x.Version)
                                    .ToArray()
                                    .Select(x => _serializer.Deserialize(x.Payload, Type.GetType(x.EventType)))
                                    .Cast<IEvent>()
                                    .ToArray();

                if (!events.Any())
                {
                    throw new InvalidOperationException("Aggregate no found: " + id);
                }

                return (T)Activator.CreateInstance(typeof(T), new object[] { id, events });
            }
        }

        public IAdvancedEventStore Advanced
        {
            get { return this; }
        }

        void IAdvancedEventStore.ReplayEvents()
        {
            var count = 0;
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            using (var context = new EventStoreDbContext())
            {
                
                Parallel.ForEach(context.Events
                    .OrderBy(x => x.AggregateId)
                    .ThenBy(x => x.Version)
                    .GroupBy(x => x.AggregateId), group =>
                    {
                        foreach (var @event in group)
                        {
                            var payload =
                                (IEvent) _serializer.Deserialize(@event.Payload, Type.GetType(@event.EventType));
                            _eventBus.Publish(payload);

                            if ((++count)%100 == 0)
                            {
                                Console.WriteLine("Published " + count + " events in " + stopwatch.Elapsed.TotalSeconds +
                                                  "s (" + (count/stopwatch.Elapsed.TotalSeconds).ToString("0.00") +
                                                  " events/s)");
                            }
                        }
                    });

            }
            Console.WriteLine("Published " + count + " events in " + stopwatch.Elapsed.TotalSeconds + "s");
            stopwatch.Stop();
        }
    }
}
