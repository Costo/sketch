using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Sketch.Core.Database;
using Sketch.Core.ReadModel;
using Sketch.Core.ReadModel.Impl;

namespace Sketch.StockPhotoImporter
{
    public class Module
    {
        public void Init(IUnityContainer container)
        {
            Func<DbContext> contextFactory = () => new SketchDbContext();
            container.RegisterType<IStockPhotoDao, StockPhotoDao>(new InjectionConstructor(contextFactory));
        }
    }
}
