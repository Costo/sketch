using System;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Domain
{
    public class StockPhoto : AggregateRoot
    {
        public StockPhoto(Guid id)
            : base(id)
        {
            this.Handles<StockPhotoCreated>(NoOp);

        }

        public StockPhoto(Guid id, string uniqueId, string pageUrl, string category, string rating, string publishedDate) 
            : this(id)
        {
            this.Update(new StockPhotoCreated
            {
                UniqueId = uniqueId,
                PageUrl = pageUrl,
                Rating = rating,
                Category = category,
                PublishedDate = publishedDate,
                ImportedDate = DateTime.UtcNow,
            });
        }
    }
}
