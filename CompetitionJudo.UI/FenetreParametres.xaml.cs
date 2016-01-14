using CompetitionJudo.Business;
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

namespace CompetitionJudo.UI
{
    /// <summary>
    /// Logique d'interaction pour FenetreParametres.xaml
    /// </summary>
    public partial class FenetreParametres : Window
    {
        private Action<NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats { get; set; }
        private Action<int> actionUpdateNbJudokas { get; set; }
        private int NbJudokasParPoule { get; set; }
        private NewDictionary<Categories, TimeSpan2> tempsCombats { get; set; }
        private Dictionary<Categories, TimeSpan> tempsCombatsDict = new Dictionary<Categories,TimeSpan>();


        public FenetreParametres()
        {
            InitializeComponent();
            InitializePerso();
        }
        
        public FenetreParametres(Action<NewDictionary<Categories, TimeSpan2>> actionUpdateTempsCombats,
                                Action<int> actionUpdateNbJudokas,
                                NewDictionary<Categories, TimeSpan2> tempsCombat,
                                int NbrJudokas)
        {
            InitializeComponent();
            
            this.actionUpdateTempsCombats = actionUpdateTempsCombats;
            this.actionUpdateNbJudokas = actionUpdateNbJudokas;
            this.NbJudokasParPoule = NbrJudokas;
            this.tempsCombats = tempsCombat;

            InitializePerso();
        }

        private void InitializePerso()
        {
            ListeCategories.ItemsSource = (List<Categories>)Enum.GetValues(typeof(Categories)).Cast<Categories>().ToList();
            foreach (var item in tempsCombats.ToDictionary())
            {
                tempsCombatsDict.Add(item.Key, item.Value.TimeSinceLastEvent);
            }
            ListeCategories.SelectedIndex = 1;

            switch (NbJudokasParPoule)  
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
            Dictionary<Categories, TimeSpan2> dic1 = new Dictionary<Categories, TimeSpan2>();

            foreach (var item in tempsCombatsDict)
            {
                dic1.Add(item.Key, new TimeSpan2(0, item.Value.Minutes, item.Value.Seconds));
            }

            var newDic = new NewDictionary<Categories, TimeSpan2>(dic1);

            
            NbJudokasParPoule = Convert.ToInt16(ListeNombreJudokasParPoule.Text);
            actionUpdateNbJudokas(NbJudokasParPoule);
            actionUpdateTempsCombats(newDic);
            this.Close();
        }

        private void ListeCategories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((Categories)ListeCategories.SelectedItem != Categories.Tous)
            {
                TxtMinutes.Text = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Minutes.ToString();
                TxtSecondes.Text = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Seconds.ToString();
            }
            else
            {
                TxtMinutes.Text = "";
                TxtSecondes.Text = "";
            }

        }

        private void ListeNombreJudokasParPoule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
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

        private void MàJTemps()
        {
            TxtSecondes.Text = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Seconds.ToString();
            TxtMinutes.Text = tempsCombatsDict[(Categories)ListeCategories.SelectedItem].Minutes.ToString();
        }
    }
}
