using Sketch.Core.Entities;

namespace Sketch.Core.ReadModel
{
    public interface IStockPhotoDao
    {
        StockPhoto[] GetRandomStockPhotos(int count);
    }
}