using AutoMapper;
using Sketch.Web.Controllers;
using Sketch.Web.Models;

namespace Sketch.Web
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<StockPhoto, DrawingSessionModel.TimedPhoto>()
                .ForMember(x => x.Duration, opt => opt.Ignore());
        }
    }
}