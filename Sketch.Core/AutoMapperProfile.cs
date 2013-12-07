using AutoMapper;
using Sketch.Core.Events;
using Sketch.Core.ReadModel;
using System.Xml.Linq;

namespace Sketch.Core
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<StockPhotoCreated, StockPhotoDetail>()
                .ForMember(x => x.StockPhotoId, opt => opt.MapFrom(x=>x.SourceId))
                .ForMember(x => x.OEmbed, opt => opt.Ignore());

            this.CreateMap<Sketch.Core.Domain.OEmbedInfo, Sketch.Core.ReadModel.OEmbedInfoDetail>()
                .ReverseMap();
        }
    }
}
