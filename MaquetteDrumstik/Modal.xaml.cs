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
    /// Logique d'interaction pour Modal.xaml
    /// </summary>
    public partial class Modal : Window
    {
        public bool UtilisateurVeutIlQuitter;
        MainWindow a;
        
        Exercice unexo;
        public Modal(MainWindow ab, object haha)
        {
            
            
                unexo = (Exercice)haha; 
            a = ab;
            InitializeComponent();
            LaTextbox.Text = unexo.title;
          
        }
        
       

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
           
            this.Close();
            a.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            unexo.title = LaTextbox.Text;
            a.turborefresh();
            this.Close();
        }
    }
}
