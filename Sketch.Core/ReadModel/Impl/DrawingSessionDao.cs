using System;
using System.Data.Entity;

namespace Sketch.Core.ReadModel.Impl
{
    public class DrawingSessionDao : IDrawingSessionDao
    {
        readonly Func<DbContext> _contextFactory;

        public DrawingSessionDao(Func<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public DrawingSessionDetail Find(Guid id)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Set<DrawingSessionDetail>().Find(id);
            }
        }
    }
}
