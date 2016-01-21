using CompetitionJudo.Data;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.UI.ViewModel
{
    public class FenetreImportCompetiteursViewModel : BaseViewModel
    {
        private List<Competiteur> listeNouveauxCompetiteur;
        public string cheminFichier { get; set; }
        public Action<Competiteur> action;

        public FenetreImportCompetiteursViewModel(string cheminFichier, Action<Competiteur> action)
        {
            this.cheminFichier = cheminFichier;
            this.action = action;
            listeNouveauxCompetiteur = new List<Competiteur>();
            ImporterFichier();
        }

        public List<Competiteur> ListeNouveauxCompetiteur
        {
            get
            {
                return listeNouveauxCompetiteur;
            }
            set
            {
                listeNouveauxCompetiteur = value;
                OnPropertyChanged("ListeNouveauxCompetiteur");
            }
        }

        public void ImporterFichier()
        {
            string line;
            using (StreamReader reader = new StreamReader(cheminFichier, Encoding.GetEncoding(1252)))
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
                            Nom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[0].ToLower()),
                            Prenom = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[1].ToLower()),
                            Club = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(values[4].ToLower()),
                        };

                        Sexes sexOut;
                        if (Enum.TryParse(values[2], out sexOut))
                        {
                            competiteurTemporaire.Sexe = (Sexes)Enum.Parse(typeof(Sexes), values[2]);
                        }

                        double doubleOut;
                        if (double.TryParse(values[3], out doubleOut))
                        {
                            competiteurTemporaire.Poids = Convert.ToDouble(values[3]);
                        }
                        else
                        {
                            competiteurTemporaire.Poids = 0;
                        }

                        Categories categoriesOut;
                        if (Enum.TryParse(values[5], out categoriesOut))
                        {
                            competiteurTemporaire.Categorie = (Categories)Enum.Parse(typeof(Categories), values[5]);
                        }
                        listeNouveauxCompetiteur.Add(competiteurTemporaire);
                    }
                }
            }
            OnPropertyChanged("ListeNouveauxCompetiteur");
        }

    }
}
