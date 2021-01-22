using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.API
{
    //
    // RSapiExercice.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class RSapiExercice
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
        public IList<RSapiResource> resources { get; set; }

        #region Helpers methods

        public RSapiResource getThumbnailResource()
        {
            foreach(RSapiResource resource in resources)
            {
                if(resource.type == "resource.thumbnail")
                {
                    return resource;
                }
            }

            return null;
        }

        #endregion
    }
}
