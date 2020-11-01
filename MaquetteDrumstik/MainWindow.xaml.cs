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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MaquetteDrumstik
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            // instantiation de Data (qui contient les ExoBatt)
            DataExo Datas = new DataExo();
            List<ExoBatt> ListeExoBatt = new List<ExoBatt>();
            ListeExoBatt = Datas.GetExoBatterie();

            int cols = 3;
            int i = 0;
            int nombreLigne = 0;

            if (ListeExoBatt.Count % cols == 0)
            {
                nombreLigne = ListeExoBatt.Count / cols;
            }
            else
            {
                nombreLigne = ListeExoBatt.Count / cols + 1;
            }

            for (int r = 0; r < nombreLigne; r++)
            {
                TableRow tr = new TableRow();

                for (int c = 0; c < cols; c++)
                {

                    //sortie de boucle par eviter une erreur d'index
                    if (i >= ListeExoBatt.Count)
                    {
                        break;
                    }
                    Paragraph MonParagrapheTitre = new Paragraph();

                    MonParagrapheTitre.Inlines.Add(new Bold(new Run("\n" + ListeExoBatt[i].title + "   " + "\n" + "Exercice de niveau " + ListeExoBatt[i].level.ToString() + "\n" + "\n"))
                    {
                        Foreground = Brushes.WhiteSmoke

                    });

                    MonParagrapheTitre.BorderBrush = Brushes.Gold;
                    ThicknessConverter tc = new ThicknessConverter();
                    MonParagrapheTitre.BorderThickness = (Thickness)tc.ConvertFromString("0,08in");
                    MonParagrapheTitre.MouseEnter += MonParagrapheTitre_MouseEnter;
                    MonParagrapheTitre.MouseLeave += MonParagrapheTitre_MouseLeave;
                    tr.Cells.Add(new TableCell(MonParagrapheTitre));

                    //Quand la souris entre dans la carte, les bordure deviennent noire
                    void MonParagrapheTitre_MouseEnter(object sender, MouseEventArgs e)
                    {
                        ChangerContours(MonParagrapheTitre, Brushes.Black);
                    }
                    //Quand la souris sort, les bordures redeviennent jaunes
                    void MonParagrapheTitre_MouseLeave(object sender, MouseEventArgs e)
                    {
                        ChangerContours(MonParagrapheTitre, Brushes.Gold);
                    }
                    i++;
                }
                TableRowGroup trg = new TableRowGroup();
                trg.Rows.Add(tr);
                myTable.RowGroups.Add(trg);
            }
        }
        public void ChangerContours(Paragraph paragraph, SolidColorBrush brush)
        {
            paragraph.BorderBrush = brush;
            ThicknessConverter tuc = new ThicknessConverter();
            paragraph.BorderThickness = (Thickness)tuc.ConvertFromString("0,08in");
        }
    }
}
