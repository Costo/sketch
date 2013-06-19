namespace Sketch.Core.Infrastructure
{
    public interface IEventStore
    {
        void Save(IEventSourced eventSourced, string correlationId);
    }

    public interface IAdvancedEventStore
    {
        void ReplayEvents();
    }
}