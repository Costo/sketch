using Sketch.Core.Domain;
using Sketch.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sketch.Core.Events
{
    public class StockPhotoOEmbedInfoUpdated: DomainEvent
    {
        public OEmbedInfo OEmbed { get; set; }
    }
}
