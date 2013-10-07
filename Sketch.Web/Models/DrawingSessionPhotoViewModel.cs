﻿
using System;

namespace Sketch.Web.Models
{
    public class DrawingSessionPhotoViewModel
    {
        public Guid DrawingSessionId { get; set; }
        public string PhotoTitle { get; set; }
        public string ImageUrl { get; set; }
        public int DurationInMilliseconds { get; set; }
        
        public int NumberOfElapsedPhotos { get; set; }
        public int NumberOfRemaningPhotos { get; set; }

        public int TotalNumberOfPhotos
        {
            get { return NumberOfElapsedPhotos + NumberOfRemaningPhotos; }
        }
        public int IndexOfPhoto
        {
            get { return NumberOfElapsedPhotos; }
        }
        public double PercentageOfCompletion
        {
            get { return Math.Max(1.0, (double)NumberOfElapsedPhotos/TotalNumberOfPhotos*100); }
        }

        public string NextPageUrl { get; set; }
    }
}
