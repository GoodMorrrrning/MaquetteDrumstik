using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.API
{
    //
    // RSapiResource.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class RSapiResource
    {
        public int id { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
    }
}
