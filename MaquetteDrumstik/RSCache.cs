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
    //
    // RScache.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //

    class RScache 
    {

        public List<string> testings = new List<string>();

       readonly MemoryCache memCache;
       readonly string folder;
       readonly string specificFolder;
       readonly string localfiledirectory;
        readonly string localpathfile;

       

        public RScache() { 
        
            memCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());

            folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // Combine the base folder with your specific folder....
            specificFolder = Path.Combine(folder, "drums");
            Directory.CreateDirectory(specificFolder);
            Directory.CreateDirectory(Path.Combine(folder, "localfiles"));
             localfiledirectory = Path.Combine(folder, "localfiles");
             localpathfile = Path.Combine(localfiledirectory, "localfiles.json");
        }

        public List<RSapiResource> getListOfApiResources(List<RSexercice> test)
        {
            List<RSapiResource> t = new List<RSapiResource>();

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
            
                return Path.Combine(specificFolder, localPath);
            }
            memCache.Set(name, name);
            
           
            return null;
        }

       
        // Demande de téléchargement d'une image
        public string downloadThumbnailAsync(RSapiResource thumbnail)
        {
            string thumbnailLocalPath = Path.Combine(specificFolder, thumbnail.name);

            using (WebClient client = new WebClient())
            {
                Uri ur = new Uri(thumbnail.url);
                while (true)
                {
                    try
                    {
                       
                        client.DownloadFileAsync(ur, thumbnailLocalPath);
                        Task.Delay(2500).Wait();
                        break;
                    }
                    catch (Exception e)
                    {
                        MessageBox.Show(e.Message);
                    }
                }
            }



            return thumbnailLocalPath;

        }

      

        public void cacheLocalFiles(ObservableCollection<RSlocalFile> SerialiseOneLocalFile, string url, string title)
        {
            string Jison;
          
          
            using (StreamReader red = new StreamReader(Path.Combine(localfiledirectory, "files.json")))
            {
                Jison = red.ReadToEnd();
                red.Close();
            }
            if(Jison != "")
            {
                SerialiseOneLocalFile = new ObservableCollection<RSlocalFile>();
                SerialiseOneLocalFile = JsonConvert.DeserializeObject<ObservableCollection<RSlocalFile>>(Jison);
            }
            
            SerialiseOneLocalFile.Add(new RSlocalFile(url, title));

            string jison = JsonConvert.SerializeObject(SerialiseOneLocalFile.ToArray());

            System.IO.File.WriteAllText(localfiledirectory+"\\files.json", jison);
            
        }

        public ObservableCollection<RSlocalFile> RefreshLocalFiles(ObservableCollection<RSlocalFile> localFileAddedByUser, ObservableCollection<RSlocalFile> everyLocalFiles, ObservableCollection<RSlocalFile> defaultRequiredLocalFiles)
        {
            string Jison="";
            
            
            if (!File.Exists(localfiledirectory + "\\files.json"))
            {
                File.Create(Path.Combine(localfiledirectory, "files.json"));
                
            }
            else
            {
                using (StreamReader r = new StreamReader(Path.Combine(localfiledirectory, "files.json")))
                {
                    Jison = r.ReadToEnd();
                    r.Close();
                }
            }
           

            localFileAddedByUser = JsonConvert.DeserializeObject<ObservableCollection<RSlocalFile>>(Jison);
            everyLocalFiles.Clear();
            if(localFileAddedByUser != null)
            {
                foreach (var p in defaultRequiredLocalFiles.Union(localFileAddedByUser))
                    everyLocalFiles.Add(p);
            }
            else
            {
                everyLocalFiles = defaultRequiredLocalFiles;
            }
           
            return everyLocalFiles;
        }
        
    }
}

