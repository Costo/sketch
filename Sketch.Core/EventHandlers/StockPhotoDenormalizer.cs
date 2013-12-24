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
    public class StockPhotoDenormalizer: IEventHandler<StockPhotoCreated>,
        IEventHandler<StockPhotoOEmbedInfoUpdated>,
        IEventHandler<StockPhotoBanned>
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

        public void Handle(StockPhotoOEmbedInfoUpdated @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var stockPhoto = context.Set<StockPhotoDetail>().Find(@event.SourceId);
                stockPhoto.OEmbed = Mapper.Map<Sketch.Core.ReadModel.OEmbedInfoDetail>(@event.OEmbed);

                context.SaveChanges();
            }
        }

        public void Handle(StockPhotoBanned @event)
        {
            using (var context = _contextFactory.Invoke())
            {
                var stockPhoto = context.Set<StockPhotoDetail>().Find(@event.SourceId);

                context.Set<StockPhotoDetail>().Remove(stockPhoto);
                context.SaveChanges();
            }
        }
    }
}
