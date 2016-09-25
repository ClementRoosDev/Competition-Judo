using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CompetitionJudo.Data.Donnees
{
    [Serializable]
    public class Donnee
    {
        public ObservableCollection<Competiteur> ListeCompetiteurs { get; set; }
        public ObservableCollection<Groupe> ListeGroupes { get; set; }

        public string LieuCompetition { get; set; }
        public string NomCompetition { get; set; }
        public DateTime DateCompetition { get; set; }
        public string CheminFichier { get; set; }

        public int NombreParPoule { get; set; }

        public NewDictionary<Categories, TimeSpan2> TempsCombat { get; set; }
        public NewDictionary<Categories, TimeSpan2> TempsImmo { get; set; }
    }

}
