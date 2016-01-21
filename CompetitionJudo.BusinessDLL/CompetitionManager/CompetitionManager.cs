using CompetitionJudo.Data;
using CompetitionJudo.Data.Donnees;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.Business.CompetitionManager
{
    public class CompetitionManager
    {
        public Donnee CreerNouvelleCompetition(string lieu,string nomCompetition,DateTime date,string cheminFichier)
        {
            return new Donnee()
            {
                ListeCompetiteurs = new ObservableCollection<Competiteur>(),
                LieuCompetition = lieu,
                NomCompetition = nomCompetition,
                DateCompetition = date,
                CheminFichier = cheminFichier,
                NombreParPoule = 4,
                TempsCombat = new NewDictionary<Categories, TimeSpan2>(CreateTempsCombat()),
                TempsImmo = new NewDictionary<Categories, TimeSpan2>(CreateTempsImmo())
            };

        }

        private static Dictionary<Categories, TimeSpan2> CreateTempsCombat()
        {
            Dictionary<Categories, TimeSpan2> tempsCombats = new Dictionary<Categories, TimeSpan2>();
            tempsCombats.Add(Categories.Baby, new TimeSpan2(0, 1, 0));
            tempsCombats.Add(Categories.MiniPoussin, new TimeSpan2(0, 1, 15));
            tempsCombats.Add(Categories.Poussin, new TimeSpan2(0, 1, 30));
            tempsCombats.Add(Categories.Benjamin, new TimeSpan2(0, 3, 0));
            tempsCombats.Add(Categories.Minime, new TimeSpan2(0, 3, 30));
            tempsCombats.Add(Categories.Cadet, new TimeSpan2(0, 4, 0));
            tempsCombats.Add(Categories.Junior, new TimeSpan2(0, 5, 0));
            tempsCombats.Add(Categories.Senior, new TimeSpan2(0, 5, 0));
            tempsCombats.Add(Categories.Veteran, new TimeSpan2(0, 5, 0));
            return tempsCombats;
        }

        private static Dictionary<Categories, TimeSpan2> CreateTempsImmo()
        {
            Dictionary<Categories, TimeSpan2> tempsImmo = new Dictionary<Categories, TimeSpan2>();
            tempsImmo.Add(Categories.Baby, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.MiniPoussin, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Poussin, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Benjamin, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Minime, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Cadet, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Junior, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Senior, new TimeSpan2(0, 0, 20));
            tempsImmo.Add(Categories.Veteran, new TimeSpan2(0, 0, 20));
            return tempsImmo;
        }
    }
}
