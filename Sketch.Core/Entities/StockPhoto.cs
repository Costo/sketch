using System.ComponentModel.DataAnnotations;
namespace Sketch.Core.Entities
{
    public class StockPhoto
    {
        //public static StockPhoto CreateFrom(FeedItem feedItem)
        //{
        //    return new StockPhoto
        //    {
        //        ImageUrl = feedItem.Content
        //    };
        //}

        [Key]
        public string ImageUrl
        {
            get; set;
        }
    }
}