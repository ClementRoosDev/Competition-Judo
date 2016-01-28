using CompetitionJudo.Business;
using CompetitionJudo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CompetitionJudo.UI.ViewModel;

namespace CompetitionJudo.UI
{
    /// <summary>
    /// Logique d'interaction pour FenetreParametres.xaml
    /// </summary>
    public partial class FenetreParametres : Window
    {
        private FenetreParametresViewModel VM;        
        private Dictionary<Categories, TimeSpan> tempsCombatsDict = new Dictionary<Categories,TimeSpan>();
        private Dictionary<Categories, TimeSpan> tempsImmoDict = new Dictionary<Categories, TimeSpan>();


        public FenetreParametres(Action<NewDictionary<Categories, TimeSpan2>, NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats,
                                Action<int> actionUpdateNbJudokas,
                                NewDictionary<Categories, TimeSpan2> tempsCombat,
                                NewDictionary<Categories, TimeSpan2> tempsImmo,
                                int nbJudokasParPoule)
        {
            InitializeComponent();
            VM = new FenetreParametresViewModel(actionUpdateTempsCombats, actionUpdateNbJudokas,nbJudokasParPoule,tempsCombat,tempsImmo);
            this.DataContext = VM;

            InitializePerso();
        }

        private void InitializePerso()
        {
            foreach (var item in VM.TempsCombats.ToDictionary())
            {
                tempsCombatsDict.Add(item.Key, item.Value.TimeSinceLastEvent);
            }

            foreach (var item in VM.TempsImmo.ToDictionary())
            {
                tempsImmoDict.Add(item.Key, item.Value.TimeSinceLastEvent);
            }

            ListeCategories.SelectedIndex = 0;

            switch (VM.NbJudokasParPoule)  
            {
                case 2 :
                    ListeNombreJudokasParPoule.SelectedIndex = 0;
                    break;
                case 3:
                    ListeNombreJudokasParPoule.SelectedIndex = 1;
                    break;
                case 4:
                    ListeNombreJudokasParPoule.SelectedIndex = 2;
                    break;
                case 5:
                    ListeNombreJudokasParPoule.SelectedIndex = 3;
                    break;
                case 6:
                    ListeNombreJudokasParPoule.SelectedIndex = 4;
                    break;

                default:
                    break;
            }
        }

        private void ButtonAnnuler_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonOk_Click(object sender, RoutedEventArgs e)
        {
            ButtonOk.IsEnabled = false;
            Dictionary<Categories, TimeSpan2> dictionnaireTempsCombat = new Dictionary<Categories, TimeSpan2>();
            Dictionary<Categories, TimeSpan2> dictionnaireTempsImmo = new Dictionary<Categories, TimeSpan2>();

            foreach (var item in tempsCombatsDict)
            {
                dictionnaireTempsCombat.Add(item.Key, new TimeSpan2(0, item.Value.Minutes, item.Value.Seconds));
            }
            foreach (var item in tempsImmoDict)
            {
                dictionnaireTempsImmo.Add(item.Key, new TimeSpan2(0, item.Value.Minutes, item.Value.Seconds));
            }

            var dictionnaireSerialisableTempsCombat = new NewDictionary<Categories, TimeSpan2>(dictionnaireTempsCombat);
            var dictionnaireSerialisableTempsImmo = new NewDictionary<Categories, TimeSpan2>(dictionnaireTempsImmo);

            VM.NbJudokasParPoule = Convert.ToInt16(ListeNombreJudokasParPoule.Text);
            VM.ActionUpdateNbJudokas(VM.NbJudokasParPoule);
            VM.ActionUpdateTempsCombats(dictionnaireSerialisableTempsCombat, dictionnaireSerialisableTempsImmo);
            this.Close();
        }

        private void ListeCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!((Categories)ListeCategories.SelectedItem == Categories.Tous))
            {
                MàJTemps();
            }
        }

        private void UpMinute_Click(object sender, RoutedEventArgs e)
        {
            tempsCombatsDict[(Categories)ListeCategories.SelectedItem] = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, 1, 0));
            MàJTemps();
        }

        private void DownMinute_Click(object sender, RoutedEventArgs e)
        {
            if (tempsCombatsDict[(Categories)ListeCategories.SelectedItem].TotalSeconds > 59)
                tempsCombatsDict[(Categories)ListeCategories.SelectedItem] = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, -1, 0));
            MàJTemps();
        }

        private void UpSecondes_Click(object sender, RoutedEventArgs e)
        {
            tempsCombatsDict[(Categories)ListeCategories.SelectedItem] = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, 0, 15));
            MàJTemps();
        }

        private void DownSecondes_Click(object sender, RoutedEventArgs e)
        {
            if (tempsCombatsDict[(Categories)ListeCategories.SelectedItem].TotalSeconds > 14)
                tempsCombatsDict[(Categories)ListeCategories.SelectedItem] = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, 0, -15));
            MàJTemps();
        }

        private void DownSecondesImmo_Click(object sender, RoutedEventArgs e)
        {
            if (tempsImmoDict[(Categories)ListeCategories.SelectedItem].TotalSeconds > 0)
                tempsImmoDict[(Categories)ListeCategories.SelectedItem] = tempsImmoDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, 0, -5));
            MàJTemps();
        }

        private void UpSecondesImmo_Click(object sender, RoutedEventArgs e)
        {
            if (tempsImmoDict[(Categories)ListeCategories.SelectedItem].TotalSeconds < 54)
                tempsImmoDict[(Categories)ListeCategories.SelectedItem] = tempsImmoDict[(Categories)ListeCategories.SelectedItem].Add(new TimeSpan(0, 0, 5));
            MàJTemps();
        }

        private void MàJTemps()
        {
            VM.TempsCombatSecondes  = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Seconds;
            VM.TempsCombatMinutes = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Minutes;
            VM.TempsImmoSecondes = tempsImmoDict[(Categories)ListeCategories.SelectedItem].Seconds;
        }
    }
}
