using System;

namespace Sketch.Core.Infrastructure
{
    public class Command: ICommand
    {
        public Command()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}