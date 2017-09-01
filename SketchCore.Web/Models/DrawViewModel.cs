using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SketchCore.Web.Models
{
    public class DrawViewModel
    {
        public string ImageUrl { get; set; }
        public int PhotoIndex { get; set; }
        public int PhotoCount { get; set; }
        public string Next { get; set; }
        public TimeSpan Duration { get; set; }

        public double Progress
        {
            get { return Math.Max(1.0, (double)PhotoIndex / PhotoCount * 100); }
        }
    }
}
