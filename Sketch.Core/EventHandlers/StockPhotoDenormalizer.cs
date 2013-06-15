using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Sketch.Core.Domain;
using Sketch.Core.Events;
using Sketch.Core.Infrastructure;
using Sketch.Core.ReadModel;

namespace Sketch.Core.EventHandlers
{
    public class StockPhotoDenormalizer: IEventHandler<StockPhotoCreated>
    {
        readonly Func<DbContext> _contextFactory;

        public StockPhotoDenormalizer(Func<DbContext> contextFactory )
        {
            _contextFactory = contextFactory;
        }

        public void Handle(StockPhotoCreated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var detail = Mapper.Map<StockPhotoDetail>(@event);
                context.Set<StockPhotoDetail>().Add(detail);
                context.SaveChanges();
            }
        }
    }
}
