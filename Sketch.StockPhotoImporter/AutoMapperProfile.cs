using AutoMapper;
using Sketch.Core.Entities;
using Sketch.StockPhotoImporter.Syndication;

namespace Sketch.Core
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<FeedItem, StockPhoto>()
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Content));
        }
    }
}
