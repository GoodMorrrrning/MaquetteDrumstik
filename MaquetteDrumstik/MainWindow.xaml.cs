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
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        //DataExo Datas = new DataExo();
      public ObservableCollection<Exercice> exercices { get; set; } = new ObservableCollection<Exercice>();
      public ObservableCollection<LocalFile> listefiles { get; set; } = new ObservableCollection<LocalFile>();
       public ObservableCollection<LocalFile> LocalfilesAddedByUser { get; set; } = new ObservableCollection<LocalFile>();
        ObservableCollection<LocalFile> EveryListFiles { get; set; } = new ObservableCollection<LocalFile>();

        APIDrumstik api = new APIDrumstik();
        List<APIExercice> apiExercices = new List<APIExercice>();

        
        public ObservableCollection<Exercice> currentExercices { get; set; }
    = new ObservableCollection<Exercice>();

       
        public MainWindow()
        {
            listefiles.Add(new LocalFile(@"C:\Users\marti\Desktop\Drumstik\plus.png", "Ajouter un fichier local "));
            listefiles.Add(new LocalFile(@"C:\Users\marti\Desktop\Drumstik\mock1.jpg", "mocktitle1"));
            listefiles.Add(new LocalFile(@"C:\Users\marti\Desktop\Drumstik\mock2.jpg", "mocktitle2"));
            listefiles.Add(new LocalFile(@"C:\Users\marti\Desktop\Drumstik\mock3.png", "mocktitle3"));

            InitializeComponent();
          
            this.DataContext = this;
            apiExercices = api.GetExercices();
            Exercice e = new Exercice(apiExercices);
            this.MinWidth = 930;
            this.MinHeight = 450;

             scroll.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;

             _= PrintExercicesAsync();
            
            if (exercices.Count > 0)
            {
                currentExercices.Add(exercices.First());

            }
            Diapo();
            UpdateExercices(exercices);
            Cache ca = new Cache();
           
            open.ItemsSource = ca.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);
            
        }
        
        public async Task PrintExercicesAsync()
        {
            await Task.Run(() => PrintExercices());
        }
        public void PrintExercices()
        {
            Cache cache = new Cache();
            
            foreach (APIExercice apiEx in apiExercices)
            {
                Exercice exercice = new Exercice(apiEx);

                APIResource thumbnailRes = exercice.getThumbnailResource();
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
                //exercices.Add(exercice);

            }
        }

      

        public void UpdateExercices(ObservableCollection<Exercice> filteredExercices)
        {
            
            ListViewProducts.ItemsSource = filteredExercices;
           // ListViewProducts.Items.Refresh();
        }


        public ObservableCollection<Exercice> FilterExercices(string userSearch)
        {
            ObservableCollection<Exercice> FiltreGrille = new ObservableCollection<Exercice>();

            // No filter
            if(userSearch == "")
            {
                foreach(APIExercice apiEx in exercices)
                {
                    FiltreGrille.Add((Exercice)apiEx);
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

            // scroll.ScrollToLeftEnd();
            if (scroll.ScrollableWidth > pos)
            {
                pos += scroll.ScrollableWidth/6;
                scroll.ScrollToHorizontalOffset(pos);
            }

        }
        public List<APIResource> Alim(List<Exercice> test)
        {
            
            List<APIResource> t = new List<APIResource>();
           
            for(int i = 0; i < test.Count; i++)
            {
                for (int a = 0; a < test[i].resources.Count; a++)
                {
                   
                    if (test[i].resources[a].type == "resource.thumbnail")
                    {
                        //MessageBox.Show("adad");
                       
                        t.Add(test[i].resources[a]);
                       

                    }
                }
                
            }
            
            return t;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          
            
        }

        private void ListViewProducts_Loaded(object sender, RoutedEventArgs e)
        {
            
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
            Random r = new Random();

           
           
        }

        private void ATimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Random r = new Random();
            
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Clear()));
            Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices.Add(exercices[r.Next(0, exercices.Count)])));
            string a = exercices[0].videoUrl;
           
            //MessageBox.Show(exercices[0].videoUrl);
            //  Application.Current.Dispatcher.BeginInvoke(new Action(() =>  twoo.Text = currentExercices[0].title));
            // Application.Current.Dispatcher.BeginInvoke(new Action(() => this.currentExercices[0].title = twoo.Text));

            // currentExercices.Clear();
            // currentExercices.Add(exercices[r.Next(0, exercices.Count)]);
        }

        private void twoo_TextChanged(object sender, TextChangedEventArgs e)
        {
           // unexo.Items.Refresh();
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
                    LocalFile foo = new LocalFile(local.Foo.url, local.Foo.title);

                }
                if(local.Foo.url !="" && local.Foo.title != "")
                {
                    Cache ca = new Cache();
                   
                     ca.CacheLoclafiles(LocalfilesAddedByUser, local.Foo.url, local.Foo.title);
                    // listefiles.Add(new LocalFile(local.Foo.url, local.Foo.title));

                  
                    open.ItemsSource = ca.RefreshLocalFiles(LocalfilesAddedByUser, EveryListFiles, listefiles);
                }
               
            }
        }

        private void ListViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
           
            int indexliste = ListViewProducts.SelectedIndex;
            string downloadurl = exercices[indexliste].videoUrl;
            RS_VimeoExtractor tchous = new RS_VimeoExtractor();
            List<Model.Progressive> ListOfUrls = new List<Progressive>();
            ListOfUrls = tchous.Deserialise(tchous.DownLoad(downloadurl));

            PrintVideo printVideo = new PrintVideo(ListOfUrls);
            printVideo.ShowDialog();

        }

        private void unexo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ObservableCollection<Exercice> oneExercice = new ObservableCollection<Exercice>(); oneExercice = (ObservableCollection<Exercice>)unexo.ItemsSource;
            MessageBox.Show(oneExercice[0].videoUrl);
        }
    }

   
    
   
}
