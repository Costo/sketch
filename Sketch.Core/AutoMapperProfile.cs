using AutoMapper;
using Sketch.Core.Events;
using Sketch.Core.ReadModel;

namespace Sketch.Core
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<StockPhotoCreated, StockPhotoDetail>()
                .ForMember(x=>x.StockPhotoId, opt => opt.MapFrom(x=>x.SourceId));
        }
    }
}
