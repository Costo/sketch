using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketch.Core.Infrastructure
{
    public interface IEventHandlerRegistry
    {
        void Register<T>(T handler) where T : IEventHandler;
    }
}
