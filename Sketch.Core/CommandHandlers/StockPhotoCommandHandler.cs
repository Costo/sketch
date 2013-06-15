using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sketch.Core.Commands;
using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.CommandHandlers
{
    public class StockPhotoCommandHandler: ICommandHandler<ImportStockPhoto>
    {
        private readonly IEventStore _store;

        public StockPhotoCommandHandler(IEventStore store)
        {
            _store = store;
        }
        public void Handle(ImportStockPhoto command)
        {
            var stockPhoto = new StockPhoto(command.StockPhotoId, command.Title,
                                            command.Description, command.ImageUrl,
                                            command.PageUrl, command.Rating, command.PublishedDate);

            _store.Save(stockPhoto, command.Id.ToString());
        }
    }
}
