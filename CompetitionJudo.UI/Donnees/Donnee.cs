using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompetitionJudo.Business;
using System.Collections.ObjectModel;

namespace CompetitionJudo.UI
{
    [Serializable]
    public class Donnee
    {
        public List<string> listeClubs = new List<string>();
        public ObservableCollection<Competiteur> listeAllCompetiteurs {get;set;}
        
        public string lieuCompetition ;
        public string nomCompetition ;
        public DateTime dateCompetition;
        public string cheminFichier;        

        public int nombreParPoule { get; set; }
        public NewDictionary<Categories, TimeSpan2> tempsCombat { get; set; }
    }   

}
