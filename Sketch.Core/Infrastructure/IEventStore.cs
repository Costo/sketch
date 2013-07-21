using System;

namespace Sketch.Core.Infrastructure
{
    public interface IEventStore
    {
        void Save(IEventSourced eventSourced, string correlationId);
        T Get<T>(Guid id) where T:IEventSourced;
    }

    public interface IAdvancedEventStore
    {
        void ReplayEvents();
    }
}