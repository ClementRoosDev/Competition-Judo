using CompetitionJudo.Business;
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
    /// Logique d'interaction pour FenetreVide.xaml
    /// </summary>
    public partial class FenetreVide : Window
    {
        Action<Competiteur> action;
        List<Competiteur> liste;
        public FenetreVide(List<Competiteur> listeCompetiteurs, Action<Competiteur> action)
        {
            InitializeComponent();
            this.action = action;
            this.liste = listeCompetiteurs;
            GrilleCompetiteur.DataContext = liste;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            foreach (Competiteur item in liste)
	        {
                action(item);
	        }
            this.Close();
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
