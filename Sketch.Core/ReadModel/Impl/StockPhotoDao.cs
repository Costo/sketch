using System;
using System.Data.Entity;
using System.Linq;

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

        public StockPhotoDetail[] GetRandomStockPhotos(int count)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Set<StockPhotoDetail>().ToArray()
                              .OrderBy(x => _random.NextDouble())
                              .Take(count)
                              .ToArray();
            }
        }

        public bool Exists(string uniqueId)
        {
            using (var context = _contextFactory.Invoke())
            {
                return context.Set<StockPhotoDetail>().Any(x=> x.UniqueId == uniqueId);
            }
        }
    }
}