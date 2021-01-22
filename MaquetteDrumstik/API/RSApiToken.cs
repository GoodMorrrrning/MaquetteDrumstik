using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.API
{
    //
    // RSapiToken.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class RSapiToken
    {
        [JsonProperty("access_token")]
        public string AccesToken { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
        [JsonProperty("expires_in")]
        public long ExpiresIn { get; set; }
    }
}
