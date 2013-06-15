
namespace Sketch.Core.Infrastructure
{
    public interface ICommandHandlerRegistry
    {
        void Register(ICommandHandler handler);
    }
}
