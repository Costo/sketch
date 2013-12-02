using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sketch.Core.Domain
{
    public class OEmbedInfo
    {
        public string Version { get; set; }
        public string Type { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }
        public string ProviderName { get; set; }
        public string ProviderUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public int ThumbnailWidth { get; set; }
        public int ThumbnailHeight { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
