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
      
      public ObservableCollection<RSExercice> exercices { get; set; } = new ObservableCollection<RSExercice>();
      public ObservableCollection<RSLocalFile> listefiles { get; set; } = new ObservableCollection<RSLocalFile>();
       public ObservableCollection<RSLocalFile> LocalfilesAddedByUser { get; set; } = new ObservableCollection<RSLocalFile>();
        ObservableCollection<RSLocalFile> EveryListFiles { get; set; } = new ObservableCollection<RSLocalFile>();

        RSApiDrumstik api = new RSApiDrumstik();
        List<RSApiExercice> apiExercices;

        
        public ObservableCollection<RSExercice> currentExercices { get; set; }
    = new ObservableCollection<RSExercice>();

       
        public MainWindow()
        {
            listefiles.Add(new RSLocalFile(@"C:\Users\marti\Desktop\Drumstik\plus.png", "Ajouter un fichier local "));
            listefiles.Add(new RSLocalFile(@"C:\Users\marti\Desktop\Drumstik\mock1.jpg", "mocktitle1"));
            listefiles.Add(new RSLocalFile(@"C:\Users\marti\Desktop\Drumstik\mock2.jpg", "mocktitle2"));
            listefiles.Add(new RSLocalFile(@"C:\Users\marti\Desktop\Drumstik\mock3.png", "mocktitle3"));

            InitializeComponent();
          
            this.DataContext = this;
            apiExercices = api.getExercices();
           
            this.MinWidth = 930;
            this.MinHeight = 450;

             scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

             _= printExercicesAsync();
            
            if (exercices.Count > 0)
            {
                currentExercices.Add(exercices.First());

            }
            Diapo();
            UpdateExercices(exercices);
            RSCache ca = new RSCache();
           
            open.ItemsSource = ca.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);
            
        }
        
        public async Task printExercicesAsync()
        {
            await Task.Run(() => printExercices());
        }
        public void printExercices()
        {
            RSCache cache = new RSCache();
            
            foreach (RSApiExercice apiEx in apiExercices)
            {
                RSExercice exercice = new RSExercice(apiEx);

                RSApiResource thumbnailRes = exercice.getThumbnailResource();
                if (thumbnailRes != null)
                {
                    string thumbnailLocalPath = cache.getLocalPathForURL(thumbnailRes.url, thumbnailRes.name);
                    if (thumbnailLocalPath == null)
                    {
                        thumbnailLocalPath = cache.downloadThumbnailAsync(thumbnailRes);

                    }

                    exercice.ThumbnailLocalPath = thumbnailLocalPath;

                }


                Application.Current.Dispatcher.BeginInvoke(new Action(() => this.exercices.Add(exercice)));
               

            }
        }

      

        public void UpdateExercices(ObservableCollection<RSExercice> filteredExercices)
        {
            
            ListViewProducts.ItemsSource = filteredExercices;
          
        }


        public ObservableCollection<RSExercice> FilterExercices(string userSearch)
        {
            ObservableCollection<RSExercice> FiltreGrille = new ObservableCollection<RSExercice>();

            // No filter
            if(userSearch == "")
            {
                foreach(RSApiExercice apiEx in exercices)
                {
                    FiltreGrille.Add((RSExercice)apiEx);
                }
                
            } else
            {
                // Applique le filtre
                for (int i = 0; i < exercices.Count; i++)
                {
                    if (exercices[i].title.ToUpper().Contains(userSearch.ToUpper()) && userSearch != "")
                    {
                        FiltreGrille.Add(exercices[i]);
                       
                    }
                }
            }

            return FiltreGrille;
        }





        private void Recherche_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            UpdateExercices(FilterExercices(Recherche.Text));
            ListViewProducts.Items.Refresh();
        }

        private void LaToolbar_Loaded(object sender, RoutedEventArgs e)
        {
            // copié collé, sert a supprimer la fleche moche a droite de la toolbar

            ToolBar toolBar = sender as ToolBar;
            var overflowGrid = toolBar.Template.FindName("OverflowGrid", toolBar) as FrameworkElement;
            if (overflowGrid != null)
            {
                overflowGrid.Visibility = Visibility.Collapsed;
            }

            var mainPanelBorder = toolBar.Template.FindName("MainPanelBorder", toolBar) as FrameworkElement;
            if (mainPanelBorder != null)
            {
                mainPanelBorder.Margin = new Thickness(0);
            }

        }
        double pos = 0;
        
        private void SliderGauche_Click(object sender, RoutedEventArgs e)
        {
            if (pos > 0)
            {
                pos += -300;
                scroll.ScrollToHorizontalOffset(pos);
            }


        }

        private void sliderDroit_Click(object sender, RoutedEventArgs e)
        {

           
            if (scroll.ScrollableWidth > pos)
            {
                pos += scroll.ScrollableWidth/6;
                scroll.ScrollToHorizontalOffset(pos);
            }

        }
        public List<RSApiResource> getListOfResource(List<RSExercice> test)
        {
            
            List<RSApiResource> t = new List<RSApiResource>();
           
            for(int i = 0; i < test.Count; i++)
            {
                for (int a = 0; a < test[i].resources.Count; a++)
                {
                   
                    if (test[i].resources[a].type == "resource.thumbnail")
                    {
                        
                       
                        t.Add(test[i].resources[a]);
                       

                    }
                }
                
            }
            
            return t;
        }

       

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            api.refreshToken();
        }

        public void Diapo()
        {
            System.Timers.Timer aTimer = new System.Timers.Timer(5500);
            aTimer.Elapsed += ATimer_Elapsed;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
         

           
           
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random r = new Random();
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Clear()));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Add(exercices[r.Next(0, exercices.Count)])));
          
           
            
        }

        private void twoo_TextChanged(object sender, TextChangedEventArgs e)
        {
            throw new NotSupportedException();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            if(unexo.SelectedIndex !=-1)
            {
                Modal a = new Modal(this, unexo.SelectedItem);
                a.ShowDialog();
            }
            else if(ListViewProducts.SelectedIndex != -1)
            {
                Modal a = new Modal(this, ListViewProducts.SelectedItem);
                a.ShowDialog();
            }
            else
            {
                MessageBox.Show("Rien n'est sélectionné");
            }

            
           
           
            
        }

       public void turborefresh()
        {
            ListViewProducts.Items.Refresh();
        }

        private void open_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
          
           if(open.SelectedIndex == 0)
            {
                AddLocalFile local = new AddLocalFile();
                if(local.ShowDialog() == true)
                {
                    _ = new RSLocalFile(local.Foo.url, local.Foo.title);

                }
                if(local.Foo.url !="" && local.Foo.title != "")
                {
                    RSCache ca = new RSCache();
                   
                     ca.cacheLocalFiles(LocalfilesAddedByUser, local.Foo.url, local.Foo.title);
                    

                  
                    open.ItemsSource = ca.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);
                }
               
            }
        }

        private void ListViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            int indexliste = ListViewProducts.SelectedIndex;
            string downloadurl = exercices[indexliste].videoUrl;
            RSVimeoExtractor tchous = new RSVimeoExtractor();
            List<Model.Progressive> ListOfUrls = new List<Progressive>();
            ListOfUrls = tchous.Deserialise(tchous.DownLoad(downloadurl));

            PrintVideo printVideo = new PrintVideo(ListOfUrls);
            printVideo.ShowDialog();

        }

        private void unexo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<RSExercice> oneExercice = new ObservableCollection<RSExercice>(); oneExercice = (ObservableCollection<RSExercice>)unexo.ItemsSource;
            MessageBox.Show(oneExercice[0].videoUrl);
        }
    }

   
    
   
}
