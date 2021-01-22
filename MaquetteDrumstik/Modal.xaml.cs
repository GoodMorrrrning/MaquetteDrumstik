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
    //
    // Modal.xaml.cs
    // Drumstik
    //
    // Created by martin on 22/10/2020.
    // Copyright (c) 2021 Rimsoft. All rights reserved.
    //
    public partial class Modal : Window
    {
        public bool isUserLeaving;
        MainWindow mainWin;
        
        RSexercice oneExercice;
        public Modal(MainWindow modalWin, object ex)
        {                    
            oneExercice = (RSexercice)ex; 
            mainWin = modalWin;
            InitializeComponent();
            titleBox.Text = oneExercice.title;        
        }
        
       

       

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {           
            this.Close();
            mainWin.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            oneExercice.title = titleBox.Text;
            mainWin.refreshExcercices();
            this.Close();
        }
    }
}
