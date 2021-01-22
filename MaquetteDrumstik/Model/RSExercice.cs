using MaquetteDrumstik.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
    //
    // RSexercice.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public class RSexercice : RSapiExercice
    {
        readonly private List<RSapiExercice> _apiExercices;

        public RSexercice(RSapiExercice data)
        {
            this.id = data.id;
            this.uuid = data.uuid;
            this.title = data.title;
            this.style = data.style;
            this.level = data.level;
            this.resume = data.resume;
            this.description = data.description;
            this.videoUrl = data.videoUrl;
            this.duration = data.duration;
            this.skills = data.skills;
            this.created_at = data.created_at;
            this.updated_at = data.updated_at;
            this.title_fr = data.title_fr;
            this.resume_fr = data.resume_fr;
            this.description_fr = data.description_fr;
            this.resources = data.resources;
        }

        public RSexercice(List<RSapiExercice> apiExercices)
        {
            this._apiExercices = apiExercices;
        }

        public string ThumbnailLocalPath
        {

           
            get; set;
        }

    }
  
}


