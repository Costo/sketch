namespace Sketch.Core.ReadModel
{
    public interface IStockPhotoDao
    {
        StockPhotoDetail[] GetRandomStockPhotos(int count);
        bool Exists(string imageUrl);
    }
}