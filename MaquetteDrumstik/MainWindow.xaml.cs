using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Runtime.Caching;

using Path = System.IO.Path;
using Nancy.Json;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.ComponentModel;
using MaquetteDrumstik.API;
using MaquetteDrumstik.Model;
using Nancy.ModelBinding;
using System.Collections.ObjectModel;
using Aspose.Cells.Drawing;

namespace MaquetteDrumstik
{
    //
    // MainWindow.xaml.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public partial class MainWindow : Window
    {
      
      public ObservableCollection<RSexercice> exercices { get; set; } = new ObservableCollection<RSexercice>();
      public ObservableCollection<RSlocalFile> listefiles { get; set; } = new ObservableCollection<RSlocalFile>();
       public ObservableCollection<RSlocalFile> LocalfilesAddedByUser { get; set; } = new ObservableCollection<RSlocalFile>();
        ObservableCollection<RSlocalFile> EveryListFiles { get; set; } = new ObservableCollection<RSlocalFile>();

        RSapiDrumstik api = new RSapiDrumstik();
        List<RSapiExercice> apiExercices;

        
        public ObservableCollection<RSexercice> currentExercices { get; set; }
    = new ObservableCollection<RSexercice>();

       
        public MainWindow()
        {
            listefiles.Add(new RSlocalFile(@"C:\Users\marti\Desktop\Drumstik\plus.png", "Ajouter un fichier local "));
            listefiles.Add(new RSlocalFile(@"C:\Users\marti\Desktop\Drumstik\mock1.jpg", "mocktitle1"));
            listefiles.Add(new RSlocalFile(@"C:\Users\marti\Desktop\Drumstik\mock2.jpg", "mocktitle2"));
            listefiles.Add(new RSlocalFile(@"C:\Users\marti\Desktop\Drumstik\mock3.png", "mocktitle3"));

            InitializeComponent();
         
            this.DataContext = this;
            apiExercices = api.getExercices();
           
            this.MinWidth = 930;
            this.MinHeight = 450;

             scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

             _= printExercicesAsync();
            
            if (exercices.Count > 0) {
                currentExercices.Add(exercices.First());
            }
            sideShow();
            UpdateExercices(exercices);
            RScache cache = new RScache();
            open.ItemsSource = cache.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);      
        }
        
        public async Task printExercicesAsync()
        {
            await Task.Run(() => printExercices());
        }
        public void printExercices()
        {
            RScache cache = new RScache();
            
            foreach (RSapiExercice apiEx in apiExercices) {
                RSexercice exercice = new RSexercice(apiEx);
                RSapiResource thumbnailRes = exercice.getThumbnailResource();
                if (thumbnailRes != null) {
                    string thumbnailLocalPath = cache.getLocalPathForURL(thumbnailRes.url, thumbnailRes.name);
                    if (thumbnailLocalPath == null) {
                        thumbnailLocalPath = cache.downloadThumbnailAsync(thumbnailRes);
                    }
                    exercice.ThumbnailLocalPath = thumbnailLocalPath;
                }
                Application.Current.Dispatcher.BeginInvoke(new Action(() => this.exercices.Add(exercice)));            
            }
        }
      
        public void UpdateExercices(ObservableCollection<RSexercice> filteredExercices)
        {         
            LVexercices.ItemsSource = filteredExercices;        
        }


        public ObservableCollection<RSexercice> FilterExercices(string userSearch)
        {
            ObservableCollection<RSexercice> FiltreGrille = new ObservableCollection<RSexercice>();

            // No filter
            if(userSearch == "") {
                foreach(RSapiExercice apiEx in exercices) {
                    FiltreGrille.Add((RSexercice)apiEx);
                }
              
            } else {
                // Applique le filtre
                for (int i = 0; i < exercices.Count; i++) {
                    if (exercices[i].title.ToUpper().Contains(userSearch.ToUpper()) && userSearch != "") {
                        FiltreGrille.Add(exercices[i]);                       
                    }
                }
            }
            return FiltreGrille;
        }


        private void Recherche_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UpdateExercices(FilterExercices(Recherche.Text));
            LVexercices.Items.Refresh();
        }

        private void LaToolbar_Loaded(object sender, RoutedEventArgs e)
        {
            // copié collé, sert a supprimer la fleche moche a droite de la toolbar

            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null) {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null) {
                mainPanelBorder.Margin = new Thickness(0);
            }
        }
        double pos = 0;
        
        private void SliderGauche_Click(object sender, RoutedEventArgs e)
        {
            if (pos > 0) {
                pos += -300;
                scroll.ScrollToHorizontalOffset(pos);
            }
        }

        private void sliderDroit_Click(object sender, RoutedEventArgs e)
        {
       
            if (scroll.ScrollableWidth > pos) {
                pos += scroll.ScrollableWidth/6;
                scroll.ScrollToHorizontalOffset(pos);
            }

        }
        public List<RSapiResource> getListOfResource(List<RSexercice> exercice)
        {
            
            List<RSapiResource> apiResources = new List<RSapiResource>();
           
            for(int i = 0; i < exercice.Count; i++) {
                for (int a = 0; a < exercice[i].resources.Count; a++) {
                   
                    if (exercice[i].resources[a].type == "resource.thumbnail") {                                            
                        apiResources.Add(exercice[i].resources[a]);                     
                    }
                }
                
            }
            
            return apiResources;
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            api.refreshToken();
        }

        public void sideShow()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer(5500);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;          
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random rand = new Random();
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Clear()));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Add(exercices[rand.Next(0, exercices.Count)])));
           
        }

        private void twoo_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            if(unexo.SelectedIndex !=-1) {
                Modal mod = new Modal(this, unexo.SelectedItem);
                mod.ShowDialog();
            }
            else if(LVexercices.SelectedIndex != -1) {
                Modal mod = new Modal(this, LVexercices.SelectedItem);
                mod.ShowDialog();
            }
            else {
                MessageBox.Show("Rien n'est sélectionné");
            }

            
           
           
            
        }

       public void refreshExcercices()
        {
            LVexercices.Items.Refresh();
        }

        private void open_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
           if(open.SelectedIndex == 0) {
                AddLocalFile local = new AddLocalFile();
                if(local.ShowDialog() == true) {
                    _ = new RSlocalFile(local.Foo.url, local.Foo.title);

                }
                if(local.Foo.url !="" && local.Foo.title != "") {
                    RScache cache = new RScache();
                   
                     cache.cacheLocalFiles(LocalfilesAddedByUser, local.Foo.url, local.Foo.title);
                                     
                    open.ItemsSource = cache.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);
                }             
            }
        }

        private void ListViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            int indexlist = LVexercices.SelectedIndex;
            string downloadurl = exercices[indexlist].videoUrl;
            RSvimeoExtractor tchous = new RSvimeoExtractor();
            List<Model.Progressive> ListOfUrls = new List<Progressive>();
            ListOfUrls = tchous.Deserialise(tchous.DownLoad(downloadurl));

            PrintVideo printVideo = new PrintVideo(ListOfUrls);
            printVideo.ShowDialog();

        }

        private void unexo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<RSexercice> oneExercice = new ObservableCollection<RSexercice>(); oneExercice = (ObservableCollection<RSexercice>)unexo.ItemsSource;
            MessageBox.Show(oneExercice[0].videoUrl);
        }
    }

   
    
   
}
