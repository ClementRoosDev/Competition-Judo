using CompetitionJudo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.UI.ViewModel
{
    public class FenetreStatsViewModel : BaseViewModel
    {
        private List<Competiteur> listeCompetiteurs;
        private List<ResultatCompetition> resultatCompetition;

        public List<ResultatCompetition> ResultatClubs {
            get
            {
                return resultatCompetition;
            }
            set
            {
                resultatCompetition = value;
                OnPropertyChanged("ResultatClubs");
            }
        }

        public FenetreStatsViewModel(List<Competiteur> listeCompetiteurs)
        {
            this.listeCompetiteurs = listeCompetiteurs;
            ResultatClubs = new List<ResultatCompetition>();
        }

        public void CalculeClassement()
        {
            var listClub = listeCompetiteurs.Where(c => c.Resultat != 0 || c.Resultat != null).GroupBy(c => c.Club).Select(f => new { Club = f.Key, Moyenne = f.Average(g => g.Resultat), NombreEngages = f.Count() }).Where(c=>c.Moyenne!=null); ;
            listClub = listClub.OrderBy(c => c.Moyenne);

            int placeFinale = 1;
            foreach (var item in listClub)
            {
                ResultatClubs.Add(new ResultatCompetition { Place = placeFinale, Club = item.Club, PlaceMoyenne = Math.Round((double)item.Moyenne, 2), NombreEngages = item.NombreEngages });
                placeFinale++;
            }
        }
    }


}
