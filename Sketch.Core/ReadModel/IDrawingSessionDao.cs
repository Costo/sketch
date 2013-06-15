using System;

namespace Sketch.Core.ReadModel
{
    public interface IDrawingSessionDao
    {
        DrawingSessionDetail Find(Guid id);
    }
}