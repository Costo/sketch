using System;
using System.Data.Entity;
using System.Linq;
using Sketch.Core.Entities;

namespace Sketch.Core.ReadModel.Impl
{
    public class StockPhotoDao : IStockPhotoDao
    {
        readonly Func<DbContext> _contextFactory;
        readonly Random _random;

        public StockPhotoDao(Func<DbContext> contextFactory)
        {
            _contextFactory = contextFactory;
            _random = new Random();
        }

        public StockPhoto[] GetRandomStockPhotos(int count)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Set<StockPhoto>().ToArray()
                              .OrderBy(x => _random.NextDouble())
                              .Take(count)
                              .ToArray();
            }
        }
    }
}