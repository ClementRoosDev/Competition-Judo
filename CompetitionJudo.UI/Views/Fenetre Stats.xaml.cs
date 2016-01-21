using CompetitionJudo.Business;
using CompetitionJudo.Data;
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

namespace CompetitionJudo.UI
{
    /// <summary>
    /// Logique d'interaction pour Fenetre_Stats.xaml
    /// </summary>
    public partial class Fenetre_Stats : Window
    {
        private List<ResultatCompetition> resultatClubs;

        public Fenetre_Stats(List<ResultatCompetition> resultatClubs)
        {
            InitializeComponent();
            this.resultatClubs = resultatClubs;
        }

        public void loadDatas()
        {
            GrilleResultats.DataContext = resultatClubs;
        }

        private void ButtonImprimerStats_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                System.Windows.Controls.PrintDialog Printdlg = new System.Windows.Controls.PrintDialog();
                if ((bool)Printdlg.ShowDialog().GetValueOrDefault())
                {
                    Size pageSize = new Size(Printdlg.PrintableAreaWidth, Printdlg.PrintableAreaHeight);

                    GrilleResultats.Measure(pageSize);
                    GrilleResultats.Arrange(new Rect(10, 10, pageSize.Width, pageSize.Height));
                    Printdlg.PrintVisual(GrilleResultats, Title);
                }
                this.Close();
            }
            catch (Exception)
            {
                this.Close();                
            }
            
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


    }
}
