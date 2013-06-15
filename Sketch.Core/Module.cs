using System;
using System.Data.Entity;
using AutoMapper;
using Microsoft.Practices.Unity;
using Sketch.Core.CommandHandlers;
using Sketch.Core.Database;
using Sketch.Core.EventHandlers;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;
using Sketch.Core.ReadModel.Impl;

namespace Sketch.Core
{
    public class Module
    {
        public void Init(IUnityContainer container)
        {
            var profile = new AutoMapperProfile();
            Mapper.AddProfile(profile);
            Mapper.AssertConfigurationIsValid(profile.ProfileName);

            Func<DbContext> contextFactory = () => new SketchDbContext();

            container.RegisterType<ICommandHandler, DrawingSessionCommandHandler>("DrawingSessionCommandHandler");
            container.RegisterType<ICommandHandler, StockPhotoCommandHandler>("StockPhotoCommandHandler");

            container.RegisterType<IEventHandler, StockPhotoDenormalizer>("StockPhotoDenormalizer", new InjectionConstructor(contextFactory));
            container.RegisterType<IEventHandler, DrawingSessionDenormalizer>("DrawingSessionDenormalizer", new InjectionConstructor(contextFactory));

            container.RegisterType<IStockPhotoDao, StockPhotoDao>(new InjectionConstructor(contextFactory));
            container.RegisterType<IDrawingSessionDao, DrawingSessionDao>(new InjectionConstructor(contextFactory));

        }
    }
}
