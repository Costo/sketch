﻿using AutoMapper;
using Sketch.Core.Commands;
using Sketch.StockPhotoImporter.Syndication;

namespace Sketch.Core
{
    public class AutoMapperProfile: Profile
    {
        protected override void Configure()
        {
            this.CreateMap<FeedItem, ImportStockPhoto>()
                .ForMember(x => x.Id, opt=>opt.Ignore())
                .ForMember(x => x.StockPhotoId, opt => opt.Ignore())
                .ForMember(x => x.ImageUrl, opt => opt.MapFrom(x => x.Content))
                .ForMember(x => x.Title, opt => opt.MapFrom(x => x.Title))
                .ForMember(x => x.Description, opt => opt.MapFrom(x => x.Description))
                .ForMember(x => x.PageUrl, opt => opt.MapFrom(x => x.Link))
                .ForMember(x => x.PublishedDate, opt => opt.MapFrom(x => x.PubDate));
        }
    }
}
