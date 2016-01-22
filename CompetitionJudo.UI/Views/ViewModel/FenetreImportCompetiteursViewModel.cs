using CompetitionJudo.Business.File_Import;
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

        public async void ImporterFichier()
        {
            ExcelImport Import = new ExcelImport();
            //await Import.ImporterCSV(cheminFichier);
            
            Import.ImporterXLS(cheminFichier);

            OnPropertyChanged("ListeNouveauxCompetiteur");

        }
    }
}
