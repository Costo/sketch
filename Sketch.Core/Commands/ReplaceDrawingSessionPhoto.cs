using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Commands
{
    public class ReplaceDrawingSessionPhoto : Command
    {
        public Guid DrawingSessionId { get; set; }
        public int IndexOfPhotoToReplace { get; set; }
    }
}
