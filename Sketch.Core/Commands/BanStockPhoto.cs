using System;
using Sketch.Core.Infrastructure;

namespace Sketch.Core.Commands
{
    public  class BanStockPhoto: Command
    {
        public Guid StockPhotoId { get; set; }
    }
}