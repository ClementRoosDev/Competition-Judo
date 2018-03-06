using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using CompetitionJudo.Data.Donnees;
using CompetitionJudo.Data;
using CompetitionJudo.UI.ViewModel;
using System.Collections.ObjectModel;

namespace CompetitionJudo.UI
{
    public partial class FenetreCompetition : Window
    {
        #region Private Properties 

        private FenetreCompetitionViewModel VM;

        #endregion

        #region Constructor
        public FenetreCompetition(Donnee donnee)
        {
            InitializeComponent();
            VM = new FenetreCompetitionViewModel(donnee);
            DataContext = VM;

            FiltreColonneSexe.ItemsSource = VM.ListeSexes;
            FiltreColonneEstPrésent.ItemsSource = VM.ListePresence;
            FiltreColonneCategorie.ItemsSource = VM.ListeCategories;
        }
        #endregion

        #region Focus sur les champs de texte

        private void texteNom_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        private void textePrenom_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        private void textePoids_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
        }

        private void listeClub_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (ComboBox)sender;
            tb.Text = string.Empty;
        }

        private void listeCategorie_GotFocus(object sender, RoutedEventArgs e)
        {
            var tb = (ComboBox)sender;
            tb.Text = string.Empty;
        }
                
        private void textePoids_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!Char.IsDigit(c) && c != ',' && c != '.')
                {
                    e.Handled = true;
                }
            }
        }
        
        #endregion

        #region Filtres Grille

        private void FiltresGrille_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FiltreColonneEstPrésent.SelectedValue != null && FiltreColonneSexe.SelectedValue != null && FiltreColonneCategorie.SelectedValue != null)
            {
                VM.FiltreNom = FiltreColonneNom.Text;
                VM.FiltrePrenom = FiltreColonnePrenom.Text;
                VM.FiltreCategorie = FiltreColonneCategorie.SelectedValue.ToString();
                VM.FiltreSexe = FiltreColonneSexe.SelectedValue.ToString();
                VM.FiltrePresence = FiltreColonneEstPrésent.SelectedValue.ToString();
            }
        }

        private void FiltreColonnesNomPrenom_TextChanged(object sender, TextChangedEventArgs e)
        {
            FiltresGrille_SelectionChanged(null, null);
        }

        #endregion

        #region Methods

        //Supprimer Competiteur
        private void boutonSupprimerCompetiteur_Click(object sender, RoutedEventArgs e)
        {
            var ListeCompetiteursASupprimer = new List<Competiteur>();
            if (grilleCompetiteurs.SelectedItems.Count > 0)
            {
                foreach (Competiteur competiteur in grilleCompetiteurs.SelectedItems)
                {
                    //Competiteur c = (Competiteur)grilleCompetiteurs.SelectedItem;
                    MessageBoxResult rsltMessageBox = MessageBox.Show(string.Format("Supprimer un compétiteur ? {0}{1} {2}", Environment.NewLine, competiteur.Prenom, competiteur.Nom), "", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);

                    if (rsltMessageBox == MessageBoxResult.Yes)
                    {
                        ListeCompetiteursASupprimer.Add(competiteur);
                    }
                }
            }
            foreach (var competiteur in ListeCompetiteursASupprimer)
            {
                VM.SupprimerCompetiteur(competiteur);
            }

        }

        //Import depuis fichier Excel
        private void boutonImporterExcel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Files (*.csv;*.xls;*xlsx)|*.csv;*.xls;*.xlsx";
                if ((bool)openFileDialog.ShowDialog())
                {
                    Action<Competiteur> addCompetiteursCallback = addCompetiteurs;
                    FenetreImportCompetiteurs fenetre = new FenetreImportCompetiteurs(addCompetiteursCallback, openFileDialog.FileName);
                    fenetre.ShowDialog();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Chargement : \r\n" + ex, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        //Callback pour ajouter des Competiteurs à partir de la fenetre d'import
        private void addCompetiteurs(Competiteur competiteur)
        {
            VM.QuickAddCompetiteur(competiteur);
        }

        //Callback pour mettre ) jour les temps de combats
        private void updateTempsDeCombat(NewDictionary<Categories, TimeSpan2> listeTempsCombat, NewDictionary<Categories, TimeSpan2> listeTempsImmo)
        {
            VM.Donnee.TempsCombat = listeTempsCombat;
            VM.Donnee.TempsImmo = listeTempsImmo;
        }

        //callback pour mettre à jour le nombre de judokas par poule
        private void updateNombreJudokasParPoule(int nbJudokasParPoule)
        {
            VM.Donnee.NombreParPoule = nbJudokasParPoule;
        }

        //Résultats généraux
        private void ButtonResultatClub_Click(object sender, RoutedEventArgs e)
        {
            Fenetre_Stats fenetreStats = new Fenetre_Stats(VM.Donnee.ListeCompetiteurs.ToList());
            fenetreStats.ShowDialog();
        }

        //Ouvre la fenetre des parametres
        private void boutonParametres_Click(object sender, RoutedEventArgs e)
        {
            Action<NewDictionary<Categories, TimeSpan2>, NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats = updateTempsDeCombat;
            Action<int> actionUpdateNbJudokas = updateNombreJudokasParPoule;

            FenetreParametres fen = new FenetreParametres(actionUpdateTempsCombats, actionUpdateNbJudokas, VM.Donnee.TempsCombat, VM.Donnee.TempsImmo, VM.Donnee.NombreParPoule);
            fen.ShowDialog();
        }

        private void KeyEditCompetiteur(object sender, DataGridRowEditEndingEventArgs e)
        {
            VM.EditCompetiteur();
        }

        private void ClickEditCompetiteur(object sender, System.Windows.Input.MouseEventArgs e)
        {
            VM.EditCompetiteur();
        }

        private void grilleCompetiteurs_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            VM.EditCompetiteur();
        }

        private void boutonGenererUnGroupe_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Groupe> listGroupPourImpression = new ObservableCollection<Groupe>();
            foreach (var groupe in GrilleGroupes.Items)
            {
                listGroupPourImpression.Add((Groupe)groupe);
            }
            VM.ListeGroupes = listGroupPourImpression;
        }
        
        #endregion
    }
}
