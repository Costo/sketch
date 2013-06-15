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

        public StockPhoto(Guid id, string title, string description, string imageUrl, string pageUrl, string rating, string publishedDate) 
            : this(id)
        {
            this.Update(new StockPhotoCreated
            {
                Title = title,
                Description = description,
                ImageUrl = imageUrl,
                PageUrl = pageUrl,
                Rating = rating,
                PublishedDate = publishedDate,
                ImportedDate = DateTime.UtcNow,
            });
        }
    }
}
