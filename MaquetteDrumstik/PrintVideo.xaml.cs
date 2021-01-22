using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace MaquetteDrumstik
{
    //
    // PrintVideo.xaml.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public partial class PrintVideo : Window
    {
        List<Model.Progressive> generalscope = new List<Model.Progressive>();
        string NEWmp4Url = "";
        public PrintVideo(List<Model.Progressive> urlsMp4)
        {
            generalscope = urlsMp4;
            InitializeComponent();
            
        
            List<string> Quality = new List<string>();
            Quality.Add("high");
            Quality.Add("medium");
            Quality.Add("horrible");
            comboquality.ItemsSource = Quality;
           
            videoExercices.Source = new Uri(urlsMp4[4].url.Remove(4,1));
            videoExercices.Play();
        }

        private void comboquality_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            videoExercices.Stop();

            if (comboquality.SelectedIndex == 0) {
                NEWmp4Url = generalscope[4].url.Remove(4, 1);
                videoExercices.Source = new Uri(NEWmp4Url);
                videoExercices.Play();
            }
            if (comboquality.SelectedIndex == 1) {
                NEWmp4Url = generalscope[2].url.Remove(4, 1);
                videoExercices.Source = new Uri(NEWmp4Url);
                videoExercices.Play();
            }
            if (comboquality.SelectedIndex == 2) {               
                NEWmp4Url = generalscope[0].url.Remove(4, 1);
                videoExercices.Source = new Uri(NEWmp4Url);
                videoExercices.Play();
            }

        }

       
    }
}
