using System;
using System.Data.Entity;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Sketch.Core.Database;
using Sketch.Core.ReadModel;
using Sketch.Core.ReadModel.Impl;
using Unity.Mvc4;

namespace Sketch.Web
{
  public static class Bootstrapper
  {
    public static IUnityContainer Initialize()
    {
      var container = BuildUnityContainer();

      DependencyResolver.SetResolver(new UnityDependencyResolver(container));

      return container;
    }

    private static IUnityContainer BuildUnityContainer()
    {
      var container = new UnityContainer();

      // register all your components with the container here
      // it is NOT necessary to register your controllers

      // e.g. container.RegisterType<ITestService, TestService>();    
      RegisterTypes(container);

      return container;
    }

    public static void RegisterTypes(IUnityContainer container)
    {
        Func<DbContext> contextFactory = () => new SketchDbContext();
        container.RegisterType<IStockPhotoDao, StockPhotoDao>(new InjectionConstructor(contextFactory));
    }
  }
}