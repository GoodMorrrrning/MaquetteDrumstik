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
    class RS_VimeoExtractor
    {

        public string DownLoad(string url)
        {
            WebClient client = new WebClient();
            
            url = url + @"\config";
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "VimeoTruc");
            string fileVimeConfigJson = Path.Combine(specificFolder, "vimeoConfig.json");
            Directory.CreateDirectory(specificFolder);

            client.DownloadFile(url, fileVimeConfigJson);

            return fileVimeConfigJson;
        }

        public List<Model.Progressive> Deserialise(string path)
        {
            StreamReader readjson = new StreamReader(path);
            string jison = readjson.ReadToEnd();
            readjson.Close();



            RS_DeserializeClass rS_Deserialize = new RS_DeserializeClass();





            rS_Deserialize = JsonConvert.DeserializeObject<RS_DeserializeClass>(jison);

            return rS_Deserialize.request.files.progressive;
        }



    }
}
