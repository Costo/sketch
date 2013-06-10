using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Commands
{
    public class GenerateDrawingSession: Command
    {
        public Guid DrawingSessionId { get; set; }
    }
}