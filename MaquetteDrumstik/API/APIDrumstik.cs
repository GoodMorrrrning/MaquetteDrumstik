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
using System.Timers;

namespace MaquetteDrumstik.API
{
    class APIDrumstik
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
                    APIToken test = JsonConvert.DeserializeObject<APIToken>(response.Result.Content.ReadAsStringAsync().Result);

                    return test.AccesToken;
                }
            }
        }
       
        public void refreshToken()
        {
            Properties.Settings.Default.token = GetToken();
            Properties.Settings.Default.Save();
        }
        public List<APIExercice> GetExercices()
        {
           //Properties.Settings.Default.token = GetToken();
          // Properties.Settings.Default.Save();
           //request + token
           var myUri = new Uri("https://api.drumstik.app/api/exercice");
            var myWebRequest = WebRequest.Create(myUri);
            var myHttpWebRequest = (HttpWebRequest)myWebRequest;
            myHttpWebRequest.PreAuthenticate = true;
            myHttpWebRequest.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.token);
            myHttpWebRequest.Accept = "application/json";

            var myWebResponse = myWebRequest.GetResponse();
            var responseStream = myWebResponse.GetResponseStream();

            var myStreamReader = new StreamReader(responseStream, Encoding.Default);
            var json = myStreamReader.ReadToEnd();

            //deserialise
            List<APIExercice> ExoBatterie = JsonConvert.DeserializeObject<List<APIExercice>>(json);

            responseStream.Close();
            myWebResponse.Close();
            // on retorune une liste d'exercice de batterie
            return ExoBatterie;
        }
    }
}

