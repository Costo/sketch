using System;
using System.Collections.Generic;
using System.Linq;

namespace Sketch.Core.Infrastructure
{
    public class InMemoryCommandBus: ICommandBus, ICommandHandlerRegistry
    {
        readonly IList<ICommandHandler> _handlers = new List<ICommandHandler>();

        public void Send<T>(T command) where T:ICommand
        {
            var handler = (ICommandHandler<T>)_handlers.Single(x => x is ICommandHandler<T>);
            handler.Handle(command);

        }

        public void Register(ICommandHandler handler)
        {
            _handlers.Add(handler);
        }
    }
}