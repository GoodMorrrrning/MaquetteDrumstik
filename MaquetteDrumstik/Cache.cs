using MaquetteDrumstik.API;
using MaquetteDrumstik.Model;
using Microsoft.Extensions.Caching.Memory;
using Nancy.Json;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using MemoryCache = Microsoft.Extensions.Caching.Memory.MemoryCache;
using Flurl;
using Flurl.Http;

namespace MaquetteDrumstik
{
    
    class Cache 
    {

        public List<string> testings = new List<string>();

        MemoryCache memCache;
        string folder;
        string specificFolder;

        // List<ExoBatt> ExoBatts = new List<ExoBatt>();
        // MainWindow m = new MainWindow();

        public Cache() { 
        
            memCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

            folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            specificFolder = Path.Combine(folder, "drums");
            Directory.CreateDirectory(specificFolder);
        }

        public List<APIResource> Alim(List<Exercice> test)
        {
            List<APIResource> t = new List<APIResource>();

            for (int i = 0; i < test.Count; i++)
            {
                for (int a = 0; a < test[i].resources.Count; a++)
                {
                    if (test[i].resources[a].type == "resource.thumbnail")
                    {
                        t.Add(test[i].resources[a]);
                    }
                }

            }
            return t;
        }

        public string getLocalPathForURL(string url, string name)
        {
            string localPath = null;
            if(memCache.TryGetValue(name, out localPath))
            {
               // MessageBox.Show(localPath);
                return Path.Combine(specificFolder, localPath);
            }
            memCache.Set(name, name);
            
           
            return null;
        }

        private async static Task<MemoryStream> downloadImage(Uri uri)
        {
            MemoryStream memStream = new MemoryStream();

            try
            {using (HttpClient client = new HttpClient())
                {
                    var response = client.GetAsync(uri).Result;
                    {
                        response.EnsureSuccessStatusCode();
                        using (MemoryStream inputStream = new MemoryStream())
                        {
                            await inputStream.CopyToAsync(memStream).ConfigureAwait(false);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return null;
        }

        // Demande de téléchargement d'une image
        public async Task<string> downloadThumbnailAsync(APIResource thumbnail)
        {
            
            string thumbnailLocalPath = Path.Combine(specificFolder, thumbnail.name);
            bool isok = true;
            while (isok)
            {


                try
                {


                    Url url = new Url(thumbnail.url);
                    var res = await url.DownloadFileAsync(specificFolder, thumbnail.name).ConfigureAwait(false);
                    // res.AllowAnyHttpStatus();




                    Task.Delay(700).Wait();
                    isok = false;


                  

                }
                catch (Flurl.Http.FlurlHttpException e)
                {
                    MessageBox.Show(e.Message + thumbnail.id);

                    
                }
               
            }
            return thumbnailLocalPath;
           
        } 

        public async void Cacheimg(List<Exercice> exo)
        {
            
            //  ExoBatts = m.getexobatt();



            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            string specificFolder = Path.Combine(folder, "drums");

            //File.Create(specificFolder);
            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(specificFolder);

            string appdataPath = Path.Combine(specificFolder, "JsonImages.json");
            using (var httpClient = new HttpClient())
            {
                
               
                
            




       

            }
        }
    }
}

