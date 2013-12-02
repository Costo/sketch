using Sketch.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketch.Core.Commands
{
    public class DownloadOEmbedData: Command
    {
        public Guid StockPhotoId { get; set; }
    }
}
