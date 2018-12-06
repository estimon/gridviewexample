using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App1
{
    public class PhotoList
    {
        public List<Photo> ListofPhotos { get; set; }
    }


    public class Photo
    {
        public int AlbumId { get; set; } 
        public int Id { get; set;}
        public string Title { get; set; }
        public string ThumbnailURL { get; set; }
        public string url { get; set; }

    }

}