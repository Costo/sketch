using System;

namespace Sketch.Core.Infrastructure
{
    public interface ICommandBus
    {
        void Send(ICommand command);
    }

    public interface ICommand
    {
        Guid Id { get; }
    }
}
