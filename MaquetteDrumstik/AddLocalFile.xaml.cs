using MaquetteDrumstik.Model;
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
    /// <summary>
    /// Logique d'interaction pour AddLocalFile.xaml
    /// </summary>
    public partial class AddLocalFile : Window
    {
        string filename = "";
        public AddLocalFile()
        {
            InitializeComponent();

            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
                // Create OpenFileDialog 
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();



                // Set filter for file extension and default file extension 
                dlg.DefaultExt = ".png";
                dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";


                // Display OpenFileDialog by calling ShowDialog method 
                Nullable<bool> result = dlg.ShowDialog();


                // Get the selected file name and display in a TextBox 
                if (result == true)
                {
                    // Open document 
                     filename = dlg.FileName;                           
                Pathfile.Text = filename;
                }           
            
        }
        public LocalFile Foo
        {
            get { return new LocalFile(filename, resourceName.Text); }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (filename == "" || resourceName.Text == "")
            {
                MessageBox.Show("Vous n'avez pas choisi de fichier ou le titre est vide");
            }
            else
            {
                this.Close();
            }
        }
    }
}
