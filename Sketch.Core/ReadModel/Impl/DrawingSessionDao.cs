using System;
using System.Data.Entity;
using System.Linq;

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
                return context.Set<DrawingSessionDetail>()
                    .Include(x => x.Photos)
                    .SingleOrDefault(x=>x.Id == id);
            }
        }
    }
}
