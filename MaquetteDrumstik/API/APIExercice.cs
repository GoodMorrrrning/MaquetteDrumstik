using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.API
{
    public class APIExercice
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
        public IList<APIResource> resources { get; set; }

        #region Helpers methods

        public APIResource getThumbnailResource()
        {
            foreach(APIResource resource in resources)
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
