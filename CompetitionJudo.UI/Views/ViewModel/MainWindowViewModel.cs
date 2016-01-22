using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.UI.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        private string nomNouvelleCompetition;
        private string lieuNouvelleCompetition;
        private DateTime dateNouvelleCompetition;

        public string NomNouvelleCompetition
        {
            get
            {
                return nomNouvelleCompetition;
            }
            set
            {
                nomNouvelleCompetition = value;
                OnPropertyChanged("NomNouvelleCompetition");
            }
        }
        public string LieuNouvelleCompetition
        {
            get
            {
                return lieuNouvelleCompetition;
            }
            set
            {
                lieuNouvelleCompetition = value;
                OnPropertyChanged("LieuNouvelleCompetition");
            }
        }
        public DateTime DateNouvelleCompetition
        {
            get
            {
                return dateNouvelleCompetition;
            }
            set
            {
                dateNouvelleCompetition = value;
                OnPropertyChanged("DateNouvelleCompetition");
            }
        }


        private bool isLoading;

        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged("IsLoading");
                OnPropertyChanged("EnableButtons");
            }
        }
        public bool EnableButtons
        {
            get
            {
                return !isLoading;
            }            
        }

        public MainWindowViewModel()
        {
            IsLoading = false;
            DateNouvelleCompetition = DateTime.Now;
        }
    }
}
