using CompetitionJudo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.UI.ViewModel
{
    public class FenetreParametresViewModel : BaseViewModel
    {
        private int tempsCombatMinutes;
        private int tempsCombatSecondes;
        private int tempsImmoSecondes;

        public Action<NewDictionary<Categories, TimeSpan2>, NewDictionary<Categories, TimeSpan2>> ActionUpdateTempsCombats { get; set; }
        public Action<int> ActionUpdateNbJudokas { get; set; }
        public int NbJudokasParPoule { get; set; }
        public NewDictionary<Categories, TimeSpan2> TempsCombats { get; set; }
        public NewDictionary<Categories, TimeSpan2> TempsImmo { get; set; }


        public FenetreParametresViewModel(Action<NewDictionary<Categories, TimeSpan2>, NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats, Action<int> actionUpdateNbJudokas, int nbJudokasParPoule, NewDictionary<Categories, TimeSpan2> tempsCombats, NewDictionary<Categories, TimeSpan2> tempsImmo)
        {
            this.ActionUpdateNbJudokas = actionUpdateNbJudokas;
            this.ActionUpdateTempsCombats = actionUpdateTempsCombats;
            this.NbJudokasParPoule = nbJudokasParPoule;
            this.TempsCombats = tempsCombats;
            this.TempsImmo = tempsImmo;
        }

        public List<Categories> ListeCategories
        {
            get
            {
                return (List<Categories>)Enum.GetValues(typeof(Categories)).Cast<Categories>().Where(c=>c!=Categories.Tous).ToList();
            }
        }

        public int TempsCombatMinutes
        {
            get
            {
                return tempsCombatMinutes;
            }
            set
            {
                tempsCombatMinutes = value;
                OnPropertyChanged("TempsCombatMinutes");
            }
        }

        public int TempsCombatSecondes
        {
            get
            {
                return tempsCombatSecondes;
            }
            set
            {
                tempsCombatSecondes = value;
                OnPropertyChanged("TempsCombatSecondes");
            }
        }

        public int TempsImmoSecondes
        {
            get
            {
                return tempsImmoSecondes;
            }
            set
            {
                tempsImmoSecondes = value;
                OnPropertyChanged("TempsImmoSecondes");
            }
        }


    }
}
