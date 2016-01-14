using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Xml;
using System.Xml.Serialization;
using CompetitionJudo.Business;
using Microsoft.Win32;
using System.Text;
using System.Globalization;

namespace CompetitionJudo.UI
{
    public partial class FenetreCompetition : Window
    {
        #region private fields
        private List<Categories> listeCategories = Enum.GetValues(typeof(Categories)).Cast<Categories>().ToList();

        private Donnee donnee;
        private List<List<Competiteur>> listePourImpression = new List<List<Competiteur>>();

        //private List<string> listeSexes = new List<string> { "M", "F","Tous" };
        private List<Sexes> listeSexes = Enum.GetValues(typeof(Sexes)).Cast<Sexes>().ToList();

        private List<Presence> listeEstPresents = Enum.GetValues(typeof(Presence)).Cast<Presence>().ToList();

        private bool besoinEnregistrement = false;

        #endregion

        #region ctor
        public FenetreCompetition(Donnee donnee)
        {
            this.donnee = donnee;
            InitializeComponent();           
            ChargementListes();

        }
        #endregion

        #region Databind

        private void ChargementListes()
        {
            listeClub.DataContext = donnee.listeClubs;
            listeCategorie.DataContext = listeCategories;
            grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs;


            FiltreColonneCategorie.DataContext = listeCategories;
            FiltreColonneCategorie.SelectedValue = listeCategories.First();
            FiltreColonneSexe.DataContext = listeSexes;
            FiltreColonneSexe.SelectedValue = listeSexes.First();
            FiltreColonneEstPrésent.DataContext = listeEstPresents;
            FiltreColonneEstPrésent.SelectedValue = listeEstPresents.First();

            this.Title = donnee.nomCompetition + " " + String.Format("{0:dd-MM-yyyy}", donnee.dateCompetition) + " | " + donnee.lieuCompetition;

        }
        #endregion

        #region Remise à zero des champs de texte
        /**
         * Remise à zero des champs compétiteur
         */
        private void resetChamps()
        {
            textePrenom.Text = "Prenom";
            texteNom.Text = "Nom";
            textePoids.Text = "Poids";
            listeCategorie.Text = "Catégorie";
            listeClub.Text = "Club";
            checkPresent.IsChecked = false;
            barreSexe.Value = 1;
        }
        #endregion

        #region focus sur les champs de texte
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
        #endregion

        #region Ajout nouveau Compétiteur
        /*
         * Les valeur des champs sont ajoutées à un nouveau compétiteur
         * Les champs sont ensuite remis à zero
         */
        private void boutonAjouterCompetiteur_Click(object sender, RoutedEventArgs e)
        {
            bool estValide = true;

            var newCompetiteur = new Competiteur()
            {                   
                
                club = listeClub.Text,
                nom = texteNom.Text,
                prenom = textePrenom.Text,
            };
            try
            {
                newCompetiteur.categorie = (Categories)listeCategorie.SelectedValue;
            }
            catch (Exception)
            {
                estValide = false;
                MessageBox.Show("Catégorie non selectionnée", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!String.IsNullOrWhiteSpace(textePoids.Text))
                try
                {
                    textePoids.Text = textePoids.Text.Replace('.', ',');
                    newCompetiteur.poids = Convert.ToDouble(textePoids.Text);
                }
                catch (FormatException)
                {
                    estValide = false;
                    MessageBox.Show("Poids invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            


            //Infos  de la barre à curseur glissant qui itère entre 0 (masculin) et 1 (féminin)
            switch (Convert.ToInt16(barreSexe.Value))
            {
                case 0:
                    newCompetiteur.sexe = Sexes.M;
                    break;
                case 1:
                    {
                    estValide = false;
                    MessageBox.Show("Sexe invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }              
                    break;
                case 2:                       
                    newCompetiteur.sexe = Sexes.F;
                    break;  
            }


                

            if ((bool)checkPresent.IsChecked)
                newCompetiteur.estPresent = true;
            else
                newCompetiteur.estPresent = false;

            if (estValide)
            {
                //ajoutNouveauClub();
                resetChamps();
                //grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs;                
                donnee.listeAllCompetiteurs.Add(newCompetiteur);
                affichageSelectif();
                besoinEnregistrement = true;
            }
            RefreshListeClub();
        }
        #endregion

        #region Bouton ajouter competiteur check

        //Enable ou disable le bouton d'ajout des compétiteurs
        private void isButtonCompetiteurEnable()
        {
            if (texteNom != null &&
                textePrenom != null &&
                textePoids != null &&
                listeClub != null &&
                listeCategorie != null &&
                barreSexe != null &&
                boutonAjouterCompetiteur != null)
            {
                if (!String.IsNullOrWhiteSpace(texteNom.Text) &&
                !String.IsNullOrWhiteSpace(textePrenom.Text) &&
                !String.IsNullOrWhiteSpace(textePoids.Text) &&
                !String.IsNullOrWhiteSpace(listeClub.Text) &&
                !listeClub.Text.Equals("Club") &&
                !String.IsNullOrWhiteSpace(listeCategorie.Text) &&
                !listeCategorie.Text.Equals("Catégorie") &&
                !listeCategorie.Text.Equals("Tous") &&
                Convert.ToInt16(barreSexe.Value) != 1)
                {
                    boutonAjouterCompetiteur.IsEnabled = true;
                }
                else
                {
                    boutonAjouterCompetiteur.IsEnabled = false;
                }
            }
        }


        private void text_SelectionChanged(object sender, RoutedEventArgs e)
        {
            isButtonCompetiteurEnable();
        }

        private void barreSexe_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            isButtonCompetiteurEnable();
        }

        private void list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            isButtonCompetiteurEnable();
        }

        private void list_DropDownClosed(object sender, EventArgs e)
        {
            isButtonCompetiteurEnable();
        }

        #endregion

        #region Ajout nouveau Club
        /*
         * Ajoute le club courant à la liste des clubs si celui ci n'y est pas encore
         */
        private void ajoutNouveauClub()
        {
            bool clubExistant = false;
            foreach (var club in donnee.listeClubs)
            {
                if (listeClub.Text.Equals(club))
                {
                    clubExistant = true;
                }
            }
            if (!clubExistant)
            {
                donnee.listeClubs.Add(listeClub.Text);
                besoinEnregistrement = true;
            }
            donnee.listeClubs.Sort();
            listeClub.Items.Refresh();
        }
        #endregion

        #region Rafraichit liste club
        private void RefreshListeClub()
        {
            donnee.listeClubs = donnee.listeAllCompetiteurs.Select(c=>c.club).Distinct().ToList();
            donnee.listeClubs.Sort();
            listeClub.Items.Refresh();
        }
        #endregion

        #region Test sur l'input du poids
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

        private void textePoule_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            foreach (char c in e.Text)
            {
                if (!Char.IsDigit(c))
                {
                    e.Handled = true;
                }
            }
        }
        #endregion

        #region Focus sur les liste club et categories
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
        #endregion

        #region Clic enregistrement compétition
        private void boutonEnregistrerCompetition_Click(object sender, RoutedEventArgs e)
        {
            besoinEnregistrement = false;
            var serialise = new XmlSerializer(typeof(Donnee));
            var reader = new StreamWriter(donnee.cheminFichier);
            serialise.Serialize(reader, donnee);
            reader.Close();
        }
        #endregion

        #region Clic Supprimer compétiteur
        private void boutonSupprimerCompetiteur_Click(object sender, RoutedEventArgs e)
        {
            string sMessageBoxText = "Supprimer un compétiteur ?";

            MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
            MessageBoxImage icnMessageBox = MessageBoxImage.Exclamation;

            MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, "", btnMessageBox, icnMessageBox);

            switch (rsltMessageBox)
            {
                case MessageBoxResult.Yes:
                    Competiteur c = (Competiteur)grilleCompetiteurs.SelectedItem;
            
                    foreach (var item in donnee.listeAllCompetiteurs)
                    {
                        if (item.Equals(c))
                        {
                            donnee.listeAllCompetiteurs.Remove(item);
                            break;
                        }
                    }
                    affichageSelectif();
                    break;

                case MessageBoxResult.No:
                    break;
            }

            besoinEnregistrement = true;
        }
        #endregion

        #region Générer un groupe => Fenetre impression
        private void boutonGenererUnGroupe_Click(object sender, RoutedEventArgs e)
        {
            if (donnee.listeAllCompetiteurs.Where(c => c.pourImpression).Count() >= 2)
            {
                var listegroupes = new List<int>();
                foreach (var comp in donnee.listeAllCompetiteurs)
                {
                    if (comp.poule!=null)
                    {
                        if (!listegroupes.Any(c => c == comp.poule))
                        {
                            listegroupes.Add((int)comp.poule);
                        }
                    }                    
                }

                var lesGroupes = new List<Groupe>();
                foreach (var groupe in listegroupes)
                {
                    var groupeTemp = new Groupe();
                    groupeTemp.id = groupe;
                    foreach (var comp in donnee.listeAllCompetiteurs)
                    {
                        if (comp.poule == groupe)
                        {
                            if (comp.pourImpression)
                                groupeTemp.competiteurs.Add(comp);
                        }
                    }
                    lesGroupes.Add(groupeTemp);
                }
                var lesGroupesPourImpression = new List<Groupe>();

                List<int> groupesNonValides = new List<int>();
                foreach (var g in lesGroupes)
                {
                    if (g.competiteurs.Count != 0)
                    {
                        if (g.competiteurs.Count < 2 || g.competiteurs.Count > 5)
                        {
                            groupesNonValides.Add(g.id);
                        }
                        else
                        {
                            lesGroupesPourImpression.Add(g);
                        }
                    }
                }

                if (groupesNonValides.Count == 0)
                {
                    var fenetreImpression = new FenetreImpression(lesGroupesPourImpression, this.donnee);
                    fenetreImpression.Show();
                }
                else
                {
                    groupesNonValides.Sort();
                    MessageBox.Show(String.Format("{0}{1}Poules n° : {2}", "Des poules ont un mauvais nombre de compétiteurs ", Environment.NewLine, string.Join(", ", groupesNonValides), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error));
                }
            }
            else
            {

                MessageBox.Show("Pas assez de compétiteurs selectionnés pour l'impression ", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }



        }
        #endregion

        #region affichage selectif de la grille
        private void ComboBoxFiltreCategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            affichageSelectif();
        }

        //Gère l'affichage de la grille selon les listes déroulantes selectionnées
        private void affichageSelectif()
        {
            if (donnee.listeAllCompetiteurs.Count !=0)
            {
                donnee.listeAllCompetiteurs = new ObservableCollection<Competiteur>(donnee.listeAllCompetiteurs.OrderBy(c => c.nom).ToList());


                Categories filtreCategorie = (Categories)FiltreColonneCategorie.SelectedValue;
                Sexes filtreSexe = (Sexes)FiltreColonneSexe.SelectedValue;
                Presence filtreEstPresent = (Presence)FiltreColonneEstPrésent.SelectedValue;

                try
                {
                    if (filtreEstPresent == Presence.Tous)
                    {
                        if (filtreSexe == Sexes.Tous)
                        {
                            if (filtreCategorie == Categories.Tous)
                                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.ToList();
                            else
                                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.categorie == filtreCategorie).ToList();
                        }
                        else
                        {
                            if (filtreCategorie == Categories.Tous)
                                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(d => d.sexe == filtreSexe).ToList();
                            else
                                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.categorie == filtreCategorie && c.sexe == filtreSexe).ToList();

                        }
                    }
                    else
                    {
                        if (filtreEstPresent == Presence.Présent)
                        {
                            if (filtreSexe == Sexes.Tous)
                            {
                                if (filtreCategorie == Categories.Tous)
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.estPresent).ToList();
                                else
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.categorie == filtreCategorie && c.estPresent).ToList();
                            }
                            else
                            {
                                if (filtreCategorie == Categories.Tous)
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.estPresent && c.sexe == filtreSexe).ToList();
                                else
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.estPresent && c.categorie == filtreCategorie && c.sexe == filtreSexe).ToList();
                            }
                        }
                        else
                        {
                            if (filtreSexe == Sexes.Tous)
                            {
                                if (filtreCategorie == Categories.Tous)
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => !c.estPresent).ToList();
                                else
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.categorie == filtreCategorie && !c.estPresent).ToList();
                            }
                            else
                            {
                                if (filtreCategorie == Categories.Tous)
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => !c.estPresent && c.sexe == filtreSexe).ToList();
                                else
                                    grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.categorie == filtreCategorie && !c.estPresent && c.sexe == filtreSexe).ToList();
                            }
                        }
                    }

                }
                catch (Exception)
                {
                }
                MajStats();
            }           

        }

        private void MajStats()
        {
            LabelInscrits.Content = donnee.listeAllCompetiteurs.Count();
            LabelPrésents.Content = donnee.listeAllCompetiteurs.Where(c => c.estPresent).Count();
        }

        private void FiltreColonneEstPrésent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            affichageSelectif();
        }
        #endregion

        #region reset impression
        //Retire tous les valeur impression = true
        private void boutonResetImpression_Click(object sender, RoutedEventArgs e)
        {
            foreach (var comp in donnee.listeAllCompetiteurs)
            {
                comp.pourImpression = false;
            }
            besoinEnregistrement = true;
            affichageSelectif();
        }
        #endregion

        #region nbw methods

        //Trier les compétiteurs présents par poids, catégorie et sexe et leur attribuer un numéro de poule
        private void boutonGenererPoules_Click(object sender, RoutedEventArgs e)
        {
            donnee.listeAllCompetiteurs = new ObservableCollection<Competiteur>(donnee.listeAllCompetiteurs.OrderBy(c => c.categorie).ThenBy(d => d.sexe).ThenBy(f => f.poids).ToList());            
            List<Sexes> listeSexesShort = new List<Sexes> { Sexes.M, Sexes.F };

            int POULE = 1;
            int COMPTEUR = 1;
            foreach (var item in donnee.listeAllCompetiteurs)
            {
                item.poule = null;
            }
            Competiteur compTemp = new Competiteur();
            bool pouleVide = true;           

            foreach (var cate in listeCategories)
            {
                foreach (var sex in listeSexesShort)
                {
                    foreach (var c in donnee.listeAllCompetiteurs.Where(c=>c.estPresent))
                    {
                        if (c.sexe == sex && c.categorie == (cate))
                        {
                            pouleVide = false;
                            c.poule = POULE;
                            COMPTEUR++;
                            if (COMPTEUR == donnee.nombreParPoule+1)
                            {
                                POULE++;
                                COMPTEUR = 1;
                            }
                        }
                    }
                    if (!pouleVide)
                    {
                        POULE++;
                        COMPTEUR = 1;
                        pouleVide = true;
                    }                    
                }
                if (!pouleVide)
                {
                    POULE++;
                    COMPTEUR = 1;
                    pouleVide = true;
                }
            }
            besoinEnregistrement = true;
            affichageSelectif();

        }

        //Filtre a partir du nom
        private void FiltreColonneNom_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (FiltreColonneNom.Text.Length > 0)
            {
                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.nom.ToLower().StartsWith(FiltreColonneNom.Text.ToLower()) == true).ToList();
            }
            else
            {
                affichageSelectif();
            }
        }

        //Filtre a partir du prenom
        private void FiltreColonnePrenom_SelectionChanged(object sender, RoutedEventArgs e)
        {
            if (FiltreColonnePrenom.Text.Length > 0)
            {
                grilleCompetiteurs.DataContext = donnee.listeAllCompetiteurs.Where(c => c.prenom.ToLower().StartsWith(FiltreColonnePrenom.Text.ToLower()) == true).ToList();
            }
            else
            {
                affichageSelectif();
            }
        }

        //Import depuis CSV
        private void boutonImporterExcel_Click(object sender, RoutedEventArgs e)
        {
            var listeNouveauxCompetiteur = new List<Competiteur>();
            try 
	        {               
		        var dialog = new OpenFileDialog();
                dialog.Filter = "csv files (*.csv)|*.csv";
                if ((bool)dialog.ShowDialog())
                {
                    string cheminFichier = dialog.FileName;
                    string line;
                    using (StreamReader reader = new StreamReader(cheminFichier,Encoding.GetEncoding(1252)))
                    {
                        string headerLine = reader.ReadLine();
                        while (!reader.EndOfStream)
                        {
                            line = reader.ReadLine();
                            var values = line.Split(';');

                            if (!String.IsNullOrWhiteSpace(values[0]) && !String.IsNullOrWhiteSpace(values[1]) && !String.IsNullOrWhiteSpace(values[5]))
                            {
                                Competiteur competiteurTemporaire = new Competiteur
                                {
                                    nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[0].ToLower()),
                                    prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[1].ToLower()),
                                    club = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[4].ToLower()),
                                };

                                Sexes sexOut;
                                if (Enum.TryParse(values[2],out sexOut))
                                {
                                    competiteurTemporaire.sexe = (Sexes)Enum.Parse(typeof(Sexes), values[2]);
                                }

                                double doubleOut;
                                if (double.TryParse(values[3],out doubleOut))
                                {
                                    competiteurTemporaire.poids = Convert.ToDouble(values[3]);
                                }
                                else
                                {
                                    competiteurTemporaire.poids = 0;
                                }

                                Categories categoriesOut;
                                if (Enum.TryParse(values[5], out categoriesOut))
                                {
                                    competiteurTemporaire.categorie = (Categories)Enum.Parse(typeof(Categories), values[5]);
                                }
                                listeNouveauxCompetiteur.Add(competiteurTemporaire);
                            }                            
                        }                            
                    }                    
                }
                
	        }
	        catch (Exception ex)
	        {
		        MessageBox.Show("Erreur Chargement :" + ex, "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);		    
	        }
            Action <Competiteur> addCompetiteursCallback = addCompetiteurs;
            besoinEnregistrement = true;
            if (listeNouveauxCompetiteur.Count>0)
            {
                FenetreVide fenetre = new FenetreVide(listeNouveauxCompetiteur, addCompetiteursCallback);
                fenetre.Show();
            }           

        }

        //Callback pour ajouter des Competiteurs à partir de la fenetre d'import
        private void addCompetiteurs(Competiteur competiteur)
        {
            Competiteur Comp = donnee.listeAllCompetiteurs.Where(c => c.nom.ToLower().Equals(competiteur.nom.ToLower()) && c.prenom.ToLower().Equals(competiteur.prenom.ToLower())).FirstOrDefault();

            if (Comp == null)
            {
                donnee.listeAllCompetiteurs.Add(competiteur);
                affichageSelectif();
            }
            else
            {
                if (MessageBox.Show(string.Format("Un judoka identique existe déjà : {0}-{1} {2} {3}kg {4} {5} Ajouter quand même : {6}-{7} {8} {9}kg {10}",
                    Environment.NewLine,Comp.nom,Comp.prenom,Comp.poids,Comp.categorie,
                    Environment.NewLine,
                    Environment.NewLine,competiteur.nom, competiteur.prenom, competiteur.poids, competiteur.categorie), "Doublon", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {
                    
                }
                else
                {
                    donnee.listeAllCompetiteurs.Add(competiteur);
                    affichageSelectif();
                }
            }
            besoinEnregistrement = true;
            
        }

        //Callback pour mettre ) jour les temps de combats
        private void updateTempsDeCombat(NewDictionary<Categories, TimeSpan2> listeTempsCombat)
        {
            donnee.tempsCombat = listeTempsCombat;
            besoinEnregistrement = true;
        }

        //callback pour mettre à jour le nombre de judokas par poule
        private void updateNombreJudokasParPoule(int nbJudokasParPoule)
        {
            donnee.nombreParPoule = nbJudokasParPoule;
            besoinEnregistrement = true;
        }

        //Résultats généraux
        private void ButtonResultatClub_Click(object sender, RoutedEventArgs e)
        {
            //List<int> data = donnee.listeAllCompetiteurs.Where(c => c.resultat != 0).GroupBy(c => new{ MyClub = c.club }).Select(f => new { Average = f.Average(p => p.resultat), Club = f.Key.MyClub}).ToList();
            var listClub = donnee.listeAllCompetiteurs.Where(c => c.resultat != 0).GroupBy(c => c.club).Select(f => new { Club = f.Key, Moyenne = f.Average(g=>g.resultat), NombreEngages = f.Count() });
            listClub=listClub.OrderBy(c => c.Moyenne);
            List<ResultatCompetition> listeResult = new List<ResultatCompetition>();
            int placeFinale = 1;
            foreach (var item in listClub)
            {
                listeResult.Add(new ResultatCompetition { place=placeFinale, club = item.Club, placeMoyenne = Math.Round((double)item.Moyenne,2), NombreEngages = item.NombreEngages });
                placeFinale++;
            }

            Fenetre_Stats fenetreStats = new Fenetre_Stats(listeResult);
            fenetreStats.loadDatas();
            fenetreStats.Show();
        }

        //coche ou dechoche toutes les checkbox impression 
        private void CheckBoxImprimer_Click(object sender, RoutedEventArgs e)
        {
            bool etat = (bool)CheckBoxImprimer.IsChecked;

            foreach (var item in grilleCompetiteurs.Items)
	        {
                if (((Competiteur)item).poule!=0)
                    ((Competiteur)item).pourImpression = etat;
	        }
            grilleCompetiteurs.Items.Refresh();
        }

        //Ouvre la fenetre des parametres
        private void boutonParametres_Click(object sender, RoutedEventArgs e)
        {
            Action<NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats = updateTempsDeCombat;
            Action<int> actionUpdateNbJudokas = updateNombreJudokasParPoule;

            FenetreParametres fen = new FenetreParametres(actionUpdateTempsCombats, actionUpdateNbJudokas,donnee.tempsCombat,donnee.nombreParPoule);
            fen.Show();

        }        

        //Windows closing
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (besoinEnregistrement)
            {
                MessageBoxResult result = MessageBox.Show("Voulez vous enregistrer vos changements ?", "", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    boutonEnregistrerCompetition_Click(null,null);
                    e.Cancel = false;                    
                }
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = false;
                }
                if (result == MessageBoxResult.Cancel)
                {
                    e.Cancel = true;
                }
            }
        }

        #endregion
    }
}
