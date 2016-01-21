﻿using CompetitionJudo.Business.Serialisation;
using CompetitionJudo.Data;
using CompetitionJudo.Data.Donnees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CompetitionJudo.UI.ViewModel
{
    public class FenetreCompetitionViewModel : BaseViewModel
    {
        #region Private Fields

        private string nouveauCompetiteurNom;
        private string nouveauCompetiteurPrenom;
        private string nouveauCompetiteurPoids;
        private string nouveauCompetiteurClub;
        private string nouveauCompetiteurCategorie;
        private int nouveauCompetiteurSexe;
        private bool nouveauCompetiteurEstPresent;

        private string filtreSexe;
        private string filtreCategorie;
        private string filtrePresence;
        private string filtreNom;
        private string filtrePrenom;

        private ICommand clickButtonAjouterCompetiteur;
        private ICommand clickButtonEnregistrerCompetition;
        private ICommand clickButtonGenererGroupes;
        private ICommand clickButtonImprimerGroupes;

        #endregion

        #region Properties 

        public Donnee Donnee
        {
            get; set;
        }

        public ICommand ClickButtonAjouterCompetiteur
        {
            get
            {
                if (clickButtonAjouterCompetiteur == null)
                {
                    clickButtonAjouterCompetiteur = new RelayCommand(p => AjouterCompetiteur());
                }
                return clickButtonAjouterCompetiteur;
            }
        }

        public ICommand ClickButtonEnregistrerCompetition
        {
            get
            {
                if (clickButtonEnregistrerCompetition == null)
                {
                    clickButtonEnregistrerCompetition = new RelayCommand(p => EnregistrerCompetition());
                }
                return clickButtonEnregistrerCompetition;
            }
        }

        public ICommand ClickGenererGroupes
        {
            get
            {
                if (clickButtonGenererGroupes == null)
                {
                    clickButtonGenererGroupes = new RelayCommand(p => GenererGroupes());
                }
                return clickButtonGenererGroupes;
            }
        }

        public ICommand ClickImprimerGroupes
        {
            get
            {
                if (clickButtonImprimerGroupes == null)
                {
                    clickButtonImprimerGroupes = new RelayCommand(p => ImprimerGroupes());
                }
                return clickButtonImprimerGroupes;
            }
        }

        public string NouveauCompetiteurNom
        {
            get
            {
                return nouveauCompetiteurNom;
            }
            set
            {
                nouveauCompetiteurNom = value;
                OnPropertyChanged("NouveauCompetiteurNom");
                OnPropertyChanged("IsCompetiteurValide");
            }
        }
        public string NouveauCompetiteurPrenom
        {
            get
            {
                return nouveauCompetiteurPrenom;
            }
            set
            {
                nouveauCompetiteurPrenom = value;
                OnPropertyChanged("NouveauCompetiteurPrenom");
                OnPropertyChanged("IsCompetiteurValide");

            }
        }
        public string NouveauCompetiteurPoids
        {
            get
            {
                return nouveauCompetiteurPoids;
            }
            set
            {
                nouveauCompetiteurPoids = value;
                OnPropertyChanged("NouveauCompetiteurPoids");
                OnPropertyChanged("IsCompetiteurValide");

            }
        }
        public string NouveauCompetiteurClub
        {
            get
            {
                return nouveauCompetiteurClub;
            }
            set
            {
                nouveauCompetiteurClub = value;
                OnPropertyChanged("NouveauCompetiteurClub");
                OnPropertyChanged("IsCompetiteurValide");
            }
        }
        public string NouveauCompetiteurCategorie
        {
            get
            {
                return nouveauCompetiteurCategorie;
            }
            set
            {
                nouveauCompetiteurCategorie = value;
                OnPropertyChanged("NouveauCompetiteurCategorie");
                OnPropertyChanged("IsCompetiteursValide");

            }
        }
        public int NouveauCompetiteurSexe
        {
            get
            {
                return nouveauCompetiteurSexe;
            }
            set
            {
                nouveauCompetiteurSexe = value;
                OnPropertyChanged("NouveauCompetiteurSexe");
                OnPropertyChanged("IsCompetiteurValide");

            }
        }
        public bool NouveauCompetiteurEstPresent
        {
            get
            {
                return nouveauCompetiteurEstPresent;
            }
            set
            {
                nouveauCompetiteurEstPresent = value;
                OnPropertyChanged("NouveauCompetiteurEstPresent");
                OnPropertyChanged("IsCompetiteurValide");
            }
        }

        public string WindowTitle
        {
            get
            {
                return string.Format("{0} {1:dd-MM-yyyy} | {2}", Donnee.NomCompetition, Donnee.DateCompetition, Donnee.LieuCompetition);
            }
        }

        public List<Sexes> ListeSexes
        {
            get
            {
                return Enum.GetValues(typeof(Sexes)).Cast<Sexes>().ToList();
            }
        }
        public List<Presence> ListePresence
        {
            get
            {
                return Enum.GetValues(typeof(Presence)).Cast<Presence>().ToList();
            }
        }
        public List<string> ListeClubs
        {
            get
            {
                return Donnee.ListeCompetiteurs.Select(c => c.Club).Distinct().ToList();
            }
        }
        public List<Categories> ListeCategories
        {
            get
            {
                return Enum.GetValues(typeof(Categories)).Cast<Categories>().ToList();
            }
        }

        public List<Categories> ListeCategoriesSansTous
        {
            get
            {
                return Enum.GetValues(typeof(Categories)).Cast<Categories>().Where(c=>c!=Categories.Tous).ToList();
            }
        }

        public string FiltrePresence
        {
            get
            {
                return filtrePresence;
            }
            set
            {
                filtrePresence = value;
                OnPropertyChanged("FiltrePresence");
                OnPropertyChanged("ListeCompetiteurs");

            }
        }
        public string FiltreCategorie
        {
            get
            {
                return filtreCategorie;
            }
            set
            {
                filtreCategorie = value;
                OnPropertyChanged("FiltreCategorie");
                OnPropertyChanged("ListeCompetiteurs");
            }
        }
        public string FiltreSexe
        {
            get
            {
                return filtreSexe;
            }
            set
            {
                filtreSexe = value;
                OnPropertyChanged("FiltreSexe");
                OnPropertyChanged("ListeCompetiteurs");
            }
        }
        public string FiltreNom
        {
            get
            {
                return filtreNom;
            }
            set
            {
                filtreNom = value;
                OnPropertyChanged("FiltreNom");
                OnPropertyChanged("ListeCompetiteurs");
            }
        }
        public string FiltrePrenom
        {
            get
            {
                return filtrePrenom;
            }
            set
            {
                filtrePrenom = value;
                OnPropertyChanged("FiltrePrenom");
            }
        }

        public ObservableCollection<Competiteur> ListeCompetiteurs
        {
            get
            {
                if (FiltreNom.Length > 0)
                {
                    var resultNom = Donnee.ListeCompetiteurs.Where(c => c.Nom.ToLower().Contains(FiltreNom.ToLower())).ToList();
                    return new ObservableCollection<Competiteur>(resultNom);
                }
                if (FiltrePrenom.Length > 0)
                {
                    var resultPrenom = Donnee.ListeCompetiteurs.Where(c => c.Prenom.ToLower().Contains(FiltrePrenom.ToLower())).ToList();
                    return new ObservableCollection<Competiteur>(resultPrenom);
                }

                var listePresence = new List<Presence>();
                var listeSexe = new List<Sexes>();
                var listeCategorie = new List<Categories>();

                listePresence.Add(ListePresence.First(p => p.ToString().Equals(FiltrePresence)));
                listeSexe.Add(ListeSexes.First(p => p.ToString().Equals(FiltreSexe)));
                listeCategorie.Add(ListeCategories.First(p => p.ToString().Equals(FiltreCategorie)));

                if (FiltrePresence.Equals("Tous"))
                {
                    listePresence.AddRange(ListePresence);
                }
                if (FiltreSexe.Equals("Tous"))
                {
                    listeSexe.AddRange(ListeSexes);
                }
                if (FiltreCategorie.Equals("Tous"))
                {
                    listeCategorie.AddRange(ListeCategories);
                }
                var result = Donnee.ListeCompetiteurs.Where(c => listeCategorie.Contains(c.Categorie) && listeSexe.Contains(c.Sexe) && listePresence.Contains(c.Presence));
                return new ObservableCollection<Competiteur>(result);
            }
            set
            {
                Donnee.ListeCompetiteurs = value;
                OnPropertyChanged("ListeCompetiteurs");
            }
        }

        public bool IsCompetiteurValide
        {
            get
            {
                if (nouveauCompetiteurNom != null
                    && nouveauCompetiteurPrenom != null
                    && nouveauCompetiteurPoids != null
                    && nouveauCompetiteurClub != null
                    && nouveauCompetiteurCategorie != null)
                {
                    if (!string.IsNullOrWhiteSpace(nouveauCompetiteurNom)
                        && !string.IsNullOrWhiteSpace(nouveauCompetiteurPrenom)
                        && !string.IsNullOrWhiteSpace(nouveauCompetiteurPoids)
                        && !string.IsNullOrWhiteSpace(nouveauCompetiteurClub)
                        && !nouveauCompetiteurClub.Equals("Club")
                        && !string.IsNullOrWhiteSpace(nouveauCompetiteurCategorie)
                        && !nouveauCompetiteurCategorie.Equals("Catégorie")
                        && !nouveauCompetiteurCategorie.Equals("Tous")
                        && nouveauCompetiteurSexe != 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                return false;
            }
        }

        public int StatsCompetiteursInscrits
        {
            get
            {
                return Donnee.ListeCompetiteurs.Count;
            }
        }

        public int StatsCompetiteursPresents
        {
            get
            {
                return Donnee.ListeCompetiteurs.Where(c => c.EstPresent).Count();
            }
        }

        private List<List<Competiteur>> listePourImpression = new List<List<Competiteur>>();

#endregion

        #region Constructor

        public FenetreCompetitionViewModel(Donnee donnee)
        {
            Donnee = donnee;
            ResetChampsNouveauCompetiteur();
        }

        #endregion

        #region Methods

        private void ResetChampsNouveauCompetiteur()
        {
            NouveauCompetiteurPrenom = "Prenom";
            NouveauCompetiteurNom = "Nom";
            NouveauCompetiteurPoids = "Poids";
            NouveauCompetiteurCategorie = "Catégorie";
            NouveauCompetiteurClub = "Club";
            NouveauCompetiteurEstPresent = false;
            NouveauCompetiteurSexe = 1;
        }

        private void AjouterCompetiteur()
        {
            bool estValide = true;

            var newCompetiteur = new Competiteur()
            {
                Club = NouveauCompetiteurClub,
                Nom = NouveauCompetiteurNom,
                Prenom = NouveauCompetiteurPrenom,
            };
            try
            {
                newCompetiteur.Categorie = ListeCategories.FirstOrDefault(c => c.ToString().Equals(NouveauCompetiteurCategorie));
            }
            catch (Exception)
            {
                estValide = false;
                //MessageBox.Show("Catégorie non selectionnée", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (!String.IsNullOrWhiteSpace(NouveauCompetiteurPoids))
                try
                {
                    NouveauCompetiteurPoids = NouveauCompetiteurPoids.Replace('.', ',');
                    newCompetiteur.Poids = Convert.ToDouble(NouveauCompetiteurPoids);
                }
                catch (FormatException)
                {
                    estValide = false;
                    //MessageBox.Show("Poids invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            //Infos  de la barre à curseur glissant qui itère entre 0 (masculin) et 1 (féminin)
            switch (NouveauCompetiteurSexe)
            {
                case 0:
                    newCompetiteur.Sexe = Sexes.M;
                    break;
                case 1:
                    {
                        estValide = false;
                        //MessageBox.Show("Sexe invalide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                case 2:
                    newCompetiteur.Sexe = Sexes.F;
                    break;
            }

            if (NouveauCompetiteurEstPresent)
                newCompetiteur.EstPresent = true;
            else
                newCompetiteur.EstPresent = false;

            if (estValide)
            {
                //grilleCompetiteurs.DataContext = donnee.ListeCompetiteurs;                
                Donnee.ListeCompetiteurs.Add(newCompetiteur);
                ResetChampsNouveauCompetiteur();
                OnPropertyChanged("ListeClubs");
                OnPropertyChanged("StatsCompetiteursPresents");
                OnPropertyChanged("StatsCompetiteursInscrits");
                OnPropertyChanged("ListeCompetiteurs");
                //affichageSelectif();
            }

        }

        private void EnregistrerCompetition()
        {
            DataSerialisation DS = new DataSerialisation();
            DS.EnregistrerCompetition(Donnee.CheminFichier, Donnee);
        }

        private void GenererGroupes()
        {
            Donnee.ListeCompetiteurs = new ObservableCollection<Competiteur>(Donnee.ListeCompetiteurs.OrderBy(c => c.Categorie).ThenBy(d => d.Sexe).ThenBy(f => f.Poids).ToList());

            int poule = 1;
            int compteur = 1;

            foreach (var competiteur in Donnee.ListeCompetiteurs)
            {
                competiteur.Poule = null;
            }

            Competiteur compTemp = new Competiteur();
            bool pouleVide = true;

            foreach (var categorie in ListeCategories)
            {
                foreach (var sexe in ListeSexes)
                {
                    foreach (var competiteur in Donnee.ListeCompetiteurs.Where(c => c.EstPresent))
                    {
                        if (competiteur.Sexe == sexe && competiteur.Categorie == (categorie))
                        {
                            pouleVide = false;
                            competiteur.Poule = poule;
                            compteur++;
                            if (compteur == Donnee.NombreParPoule + 1)
                            {
                                poule++;
                                compteur = 1;
                            }
                        }
                    }
                    if (!pouleVide)
                    {
                        poule++;
                        compteur = 1;
                        pouleVide = true;
                    }
                }
                if (!pouleVide)
                {
                    poule++;
                    compteur = 1;
                    pouleVide = true;
                }
            }

            OnPropertyChanged("ListeCompetiteurs");
        }

        public void SupprimerCompetiteur(Competiteur c)
        {
            Donnee.ListeCompetiteurs.Remove(c);
            OnPropertyChanged("ListeClubs");
            OnPropertyChanged("StatsCompetiteursInscrits");
            OnPropertyChanged("StatsCompetiteursPresents");
            OnPropertyChanged("ListeCompetiteurs");
        }

        private void ImprimerGroupes()
        {
            if (Donnee.ListeCompetiteurs.Where(c => c.PourImpression).Count() >= Donnee.NombreParPoule)
            {
                var listegroupes = new List<int>();

                foreach (var competiteur in Donnee.ListeCompetiteurs)
                {
                    if (competiteur.Poule != null)
                    {
                        if (!listegroupes.Any(c => c == competiteur.Poule))
                        {
                            listegroupes.Add((int)competiteur.Poule);
                        }
                    }
                }

                var lesGroupes = new List<Groupe>();
                foreach (var groupe in listegroupes)
                {
                    var groupeTemp = new Groupe() { MaxCompetiteursParGroupe = Donnee.NombreParPoule };
                    groupeTemp.id = groupe;
                    foreach (var comp in Donnee.ListeCompetiteurs)
                    {
                        if (comp.Poule == groupe)
                        {
                            if (comp.PourImpression)
                                groupeTemp.Competiteurs.Add(comp);
                        }
                    }
                    lesGroupes.Add(groupeTemp);
                }
                if (!lesGroupes.Any(g => !g.EstValide))
                {
                    var fenetreImpression = new FenetreImpression(lesGroupes, Donnee);
                    fenetreImpression.ShowDialog();
                }
                else
                {
                    List<int> listeGroupesNonValides = lesGroupes.Where(g => !g.EstValide).Select(g => g.id).ToList();
                    listeGroupesNonValides.Sort();
                    MessageBox.Show(String.Format("{0}{1}Poules n° : {2}", "Des poules ont un mauvais nombre de compétiteurs ", Environment.NewLine, string.Join(", ", listeGroupesNonValides), "Erreur", MessageBoxButton.OK, MessageBoxImage.Error));
                }
            }
            else
            {
                MessageBox.Show("Pas assez de compétiteurs selectionnés pour l'impression ", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        public void QuickAddCompetiteur(Competiteur competiteur)
        {
            Competiteur Comp = Donnee.ListeCompetiteurs.Where(c => c.Nom.ToLower().Equals(competiteur.Nom.ToLower()) && c.Prenom.ToLower().Equals(competiteur.Prenom.ToLower())).FirstOrDefault();

            if (Comp == null)
            {
                Donnee.ListeCompetiteurs.Add(competiteur);
                OnPropertyChanged("ListeCompetiteurs");
            }
            else
            {
                if (MessageBox.Show(string.Format("Un judoka identique existe déjà : {0}-{1} {2} {3}kg {4} {5} Ajouter quand même : {6}-{7} {8} {9}kg {10}",
                    Environment.NewLine, Comp.Nom, Comp.Prenom, Comp.Poids, Comp.Categorie,
                    Environment.NewLine,
                    Environment.NewLine, competiteur.Nom, competiteur.Prenom, competiteur.Poids, competiteur.Categorie), "Doublon", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                {

                }
                else
                {
                    Donnee.ListeCompetiteurs.Add(competiteur);
                    
                }
            }
            OnPropertyChanged("ListeCompetiteurs");
            OnPropertyChanged("ListeClubs");
        }

        #endregion
    }
}
