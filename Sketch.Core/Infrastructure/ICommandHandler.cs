namespace Sketch.Core.Infrastructure
{
    public interface ICommandHandler<in T> : ICommandHandler where T : ICommand
    {
        void Handle(T command);
    }

    public interface ICommandHandler
    {
        
    }
}