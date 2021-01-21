using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
    //
    // RSVimeoExtractor.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    class RSVimeoExtractor
    {

        public string DownLoad(string url)
        {
            using (WebClient client = new WebClient())
            {
                url = url + @"\config";
                string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string specificFolder = Path.Combine(folder, "VimeoTruc");
                string fileVimeConfigJson = Path.Combine(specificFolder, "vimeoConfig.json");
                Directory.CreateDirectory(specificFolder);

                client.DownloadFile(url, fileVimeConfigJson);

                return fileVimeConfigJson;
            }

        }

        public List<Model.Progressive> Deserialise(string path)
        {
            StreamReader readjson = new StreamReader(path);
            string jison = readjson.ReadToEnd();
            readjson.Close();



            RSDeserializeClass rS_Deserialize = new RSDeserializeClass();





            rS_Deserialize = JsonConvert.DeserializeObject<RSDeserializeClass>(jison);

            return rS_Deserialize.request.files.progressive;
        }



    }
}
