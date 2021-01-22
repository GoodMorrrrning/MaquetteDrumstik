using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
    //
    // RSlocalFile.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class RSlocalFile
    {
        public string url { get; set; }
        public string title { get; set; }

       public RSlocalFile(string pUrl, string pTitle)
        {
            url = pUrl;
            title = pTitle;
        }

    }
    
}
