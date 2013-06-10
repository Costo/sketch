using System;

namespace Sketch.StockPhotoImporter
{
    public class Clock : IClock
    {
        public DateTime UtcNow {
            get { return DateTime.UtcNow; }
        }
    }

    public interface IClock
    {
        DateTime UtcNow { get; }
    }
}
