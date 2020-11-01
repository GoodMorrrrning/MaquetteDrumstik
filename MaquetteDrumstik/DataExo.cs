using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik
{
    class DataExo
    {
        //privé car on ne doit pas l'utiliser
        private string GetToken()
        {

            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://api.drumstik.app/api/login"))
                {
                    //login

                    var contentList = new List<string>();
                    contentList.Add("email=job@drumstik.fr");
                    contentList.Add("password=Toto1234!");
                    request.Content = new StringContent(string.Join("&", contentList));
                    request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                    ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };

                
                    var response = httpClient.SendAsync(request);

                    //Deserialise
                    eltoken test = JsonConvert.DeserializeObject<eltoken>(response.Result.Content.ReadAsStringAsync().Result);

                    return test.AccesToken;
                }
            }
        }

        public List<ExoBatt> GetExoBatterie()
        {
            //request + token
            var myUri = new Uri("https://api.drumstik.app/api/exercice");
            var myWebRequest = WebRequest.Create(myUri);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + GetToken());
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            //deserialise
            List<ExoBatt> ExoBatterie = JsonConvert.DeserializeObject<List<ExoBatt>>(json);

            responseStream.Close();
            myWebResponse.Close();
            // on retorune une liste d'exercice de batterie
            return ExoBatterie;
        }


       

        }

    // création de tous les C# Objects
    public class eltoken
    {
        [JsonProperty("access_token")]
        public string AccesToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }

    public class Resourced
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
    }

    public class ExoBatt
    {
        public int id { get; set; }
        public string uuid { get; set; }
        public string title { get; set; }
        public string style { get; set; }
        public int level { get; set; }
        public string resume { get; set; }
        public string description { get; set; }
        public string videoUrl { get; set; }
        public int duration { get; set; }
        public string skills { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string title_fr { get; set; }
        public string resume_fr { get; set; }
        public string description_fr { get; set; }
        public IList<Resourced> resources { get; set; }
    }
}

