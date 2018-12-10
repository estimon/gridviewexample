using Android.App;
using Android.OS;
using Android.Widget;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Collections.Generic;
using Android.Graphics;
using Android.Support.V7.App;
using Newtonsoft.Json;
using System.Linq;

namespace App1
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected async override void OnCreate(Bundle savedInstanceState)
        {  
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            

            base.OnCreate(savedInstanceState);
            var url = "https://jsonplaceholder.typicode.com/photos";
            var photosJson = await GetAsync(url);
            var photos = JsonConvert.DeserializeObject<List<Photo>>(photosJson);
            var gridview = FindViewById<GridView>(Resource.Id.gridview);
            var progressbar = FindViewById<ProgressBar>(Resource.Id.progressBar1);
            var somePhotos = photos.Take(1);
            progressbar.Visibility = Android.Views.ViewStates.Visible;

            foreach (var photo in somePhotos)
            { 
                photo.bitmap = GetImageBitmapFromUrl(photo.url);
                System.Diagnostics.Debug.WriteLine(photo.url);
            }
            progressbar.Visibility = Android.Views.ViewStates.Gone;

            gridview.Adapter = new ImageAdapter(this, photos);



        }

        public async Task<string> GetAsync(string uri)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                return await reader.ReadToEndAsync();
            }

        }

        private Bitmap GetImageBitmapFromUrl(string url)
        {
            Bitmap imageBitmap = null;

            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(url);
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    imageBitmap = BitmapFactory.DecodeByteArray(imageBytes, 0, imageBytes.Length);
                }
            }

            return imageBitmap;
        }
    }
}