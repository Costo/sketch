using System;

namespace Sketch.Core.Infrastructure
{
    public interface ICommandBus
    {
        void Send<T>(T command) where T : ICommand;
    }

    public interface ICommand
    {
        Guid Id { get; }
    }
}
