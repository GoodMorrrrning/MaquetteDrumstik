using MaquetteDrumstik.API;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaquetteDrumstik.Model
{
    public class Exercice : APIExercice
    {
        private List<APIExercice> apiExercices;

        public Exercice(APIExercice data)
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

        public Exercice(List<APIExercice> apiExercices)
        {
            this.apiExercices = apiExercices;
        }

        public string ThumbnailLocalPath
        {

            // get { return @"C:\Users\marti\AppData\Roaming\drums\bg_2.jpg"; }
            get; set;
        }

    }
    /*
    public class I : INotifyPropertyChanged
    {


        protected void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public string ImageFullPath
        {
            get { return ThumbnailLocalPath; }
            set
            {
                if (value != ThumbnailLocalPath)
                {
                    ThumbnailLocalPath = value;
                    OnPropertyChanged("ThumbnailLocalPath");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
    */
}


