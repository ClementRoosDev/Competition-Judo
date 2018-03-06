using CompetitionJudo.Data;
using CompetitionJudo.UI.ViewModel;
using System;
using System.Windows;

namespace CompetitionJudo.UI
{
    public partial class FenetreImportCompetiteurs : Window
    {
        #region Private Properties

        private FenetreImportCompetiteursViewModel VM;

        #endregion

        #region Constructor

        public FenetreImportCompetiteurs(Action<Competiteur> action, string cheminFichier)
        {
            InitializeComponent();

            VM = new FenetreImportCompetiteursViewModel(cheminFichier,action);
            VM.ImporterFichier();
            DataContext = VM;
            MessageBox.Show($"{VM.ListeNouveauxCompetiteur.Count} compétiteurs importés", "Import", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        #endregion

        #region UI Actions (Clicks)

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
        
        #endregion
    }
}
