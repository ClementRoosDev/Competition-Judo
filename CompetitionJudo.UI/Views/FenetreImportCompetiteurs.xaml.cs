using CompetitionJudo.Business;
using CompetitionJudo.Data;
using CompetitionJudo.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
    public partial class FenetreImportCompetiteurs : Window
    {
        private FenetreImportCompetiteursViewModel VM;   
        

        public FenetreImportCompetiteurs(Action<Competiteur> action, string cheminFichier)
        {
            InitializeComponent();

            VM = new FenetreImportCompetiteursViewModel(cheminFichier,action);
            VM.ImporterFichier();
            DataContext = VM;
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            foreach (Competiteur item in VM.ListeNouveauxCompetiteur)
            {
                VM.action(item);
            }
            Close();
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }        
    }
}
