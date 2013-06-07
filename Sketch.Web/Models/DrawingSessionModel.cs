using System;
using Sketch.Web.Controllers;

namespace Sketch.Web.Models
{
    public class DrawingSessionModel
    {
        public DrawingSessionModel()
        {
        }

        public TimedPhoto[] Photos { get; set; }

        public class TimedPhoto
        {
            public TimedPhoto()
            {
                Duration = TimeSpan.FromSeconds(10).TotalMilliseconds;
            }
            public double Duration { get; set; }
            public string ImageUrl { get; set; }
        }
    }



}