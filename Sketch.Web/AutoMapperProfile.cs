using AutoMapper;
using Sketch.Core.ReadModel;
using Sketch.Web.Controllers;
using Sketch.Web.Models;

namespace Sketch.Web
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<StockPhotoDetail, DrawingSessionModel.TimedPhoto>()
                .ForMember(x => x.Duration, opt => opt.Ignore());
        }
    }
}