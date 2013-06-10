namespace Sketch.Core.Infrastructure
{
    public interface IEventStore
    {
        void Save(IEventSourced eventSourced, string correlationId);
    }
}