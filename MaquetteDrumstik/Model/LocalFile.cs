using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
   public class LocalFile
    {
        public string url { get; set; }
        public string title { get; set; }

       public LocalFile(string purl, string ptitle)
        {
            url = purl;
            title = ptitle;
        }

    }
    
}
