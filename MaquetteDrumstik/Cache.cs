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
using System.Collections.ObjectModel;

namespace MaquetteDrumstik
{
    
    class Cache 
    {

        public List<string> testings = new List<string>();

        MemoryCache memCache;
        string folder;
        string specificFolder;
        string localfiledirectory;
        string localpathfile;

        // List<ExoBatt> ExoBatts = new List<ExoBatt>();
        // MainWindow m = new MainWindow();

        public Cache() { 
        
            memCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

            folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            specificFolder = Path.Combine(folder, "drums");
            Directory.CreateDirectory(specificFolder);
            Directory.CreateDirectory(Path.Combine(folder, "localfiles"));
             localfiledirectory = Path.Combine(folder, "localfiles");
             localpathfile = Path.Combine(localfiledirectory, "localfiles.json");
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
        public string downloadThumbnailAsync(APIResource thumbnail)
        {
            string thumbnailLocalPath = Path.Combine(specificFolder, thumbnail.name);
           
            WebClient client = new WebClient();
            Uri ur = new Uri(thumbnail.url);
            while (true)
            {
                try
                {
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    client.DownloadFileAsync(ur, thumbnailLocalPath);
                    Task.Delay(2500).Wait();
                    break;
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message);
                }
            }
          
           
          
            return  thumbnailLocalPath;
           
        }

        private void Client_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
                   
        }

        public void CacheLoclafiles(ObservableCollection<LocalFile> SerialiseOneLocalFile, string url, string title)
        {
            string jisoooon;
          
            //LocalFileAddedByUser.Clear();
            using (StreamReader red = new StreamReader(Path.Combine(localfiledirectory, "files.json")))
            {
                jisoooon = red.ReadToEnd();
                red.Close();
            }
            if(jisoooon != "")
            {
                SerialiseOneLocalFile = new ObservableCollection<LocalFile>();
                SerialiseOneLocalFile = JsonConvert.DeserializeObject<ObservableCollection<LocalFile>>(jisoooon);
            }
            
            SerialiseOneLocalFile.Add(new LocalFile(url, title));

            string jison = JsonConvert.SerializeObject(SerialiseOneLocalFile.ToArray());

            System.IO.File.WriteAllText(localfiledirectory+"\\files.json", jison);
            
        }

        public ObservableCollection<LocalFile> RefreshLocalFiles(ObservableCollection<LocalFile> LocalFileAddedByUser, ObservableCollection<LocalFile> EveryLocalFiles, ObservableCollection<LocalFile> DefaultRequiredLocalFiles)
        {
            string jisoooon="";
            LocalFileAddedByUser = new ObservableCollection<LocalFile>();
            //LocalFileAddedByUser.Clear();
            if (!File.Exists(localfiledirectory + "\\files.json"))
            {
                File.Create(Path.Combine(localfiledirectory, "files.json"));
                
            }
            else
            {
                using (StreamReader r = new StreamReader(Path.Combine(localfiledirectory, "files.json")))
                {
                    jisoooon = r.ReadToEnd();
                    r.Close();
                }
            }
           

            LocalFileAddedByUser = JsonConvert.DeserializeObject<ObservableCollection<LocalFile>>(jisoooon);
            EveryLocalFiles.Clear();
            if(LocalFileAddedByUser != null)
            {
                foreach (var p in DefaultRequiredLocalFiles.Union(LocalFileAddedByUser))
                    EveryLocalFiles.Add(p);
            }
            else
            {
                EveryLocalFiles = DefaultRequiredLocalFiles;
            }
           
            return EveryLocalFiles;
        }
        
    }
}

