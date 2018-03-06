using System.IO;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using CompetitionJudo.Business.Serialisation;
using CompetitionJudo.Data.Donnees;
using System.Threading.Tasks;
using CompetitionJudo.Business.CompetitionManager;
using CompetitionJudo.UI.ViewModel;

namespace CompetitionJudo.UI
{
    public partial class MainWindow : Window
    {
        #region Private Properties

        private MainWindowViewModel VM;

        #endregion

        #region Constructor 

        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
            DataContext = VM;
        }

        #endregion

        #region Action UI (clicks)

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var fenetreNewCompetition = new FenetreCompetition(new Donnee());
            fenetreNewCompetition.Show();
            Close();
        }

        private async void OuvrirCompetition_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            if ((bool)dialog.ShowDialog())
            {
                VM.IsLoading = true;

                DataSerialisation d = new DataSerialisation();
                var result = await Task.Run(() => d.ReadFile(dialog.FileName));

                var fenetreCompetition = new FenetreCompetition(result);

                fenetreCompetition.Show();
                Close();
            }
        }

        private void NouvelleCompetition_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            dialog.FileName = string.Format("{0} {1:MM-dd-yyyy} {2}", VM.NomNouvelleCompetition, VM.DateNouvelleCompetition, VM.LieuNouvelleCompetition);

            if ((bool)dialog.ShowDialog())
            {
                VM.IsLoading = true;

                CompetitionManager CM = new CompetitionManager();
                var donneesNouvelleCompetition = CM.CreerNouvelleCompetition(VM.LieuNouvelleCompetition, VM.NomNouvelleCompetition, VM.DateNouvelleCompetition, dialog.FileName);
                DataSerialisation DS = new DataSerialisation();
                DS.EnregistrerCompetition(dialog.FileName, donneesNouvelleCompetition);
                var fenetreCompetition = new FenetreCompetition(donneesNouvelleCompetition);
                fenetreCompetition.Show();

                Close();
            }
        }

        private void BoutonTelechargerFichierImport_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "xlsx";
            saveFileDialog.Filter = "Excel files (*.xlsx)|*xlsx";
            saveFileDialog.FileName = "Inscription_Tournoi_Judo";

            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, Properties.Resources.Inscription_Tournoi_Judo);
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus_1;
        }


        private void BoutonAide_Click(object sender, RoutedEventArgs e)
        {
            var saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = "pptx";
            saveFileDialog.Filter = "Powerpoint files (*.pptx)|*pptx";
            saveFileDialog.FileName = "Competition_Judo_Guide";

            if ((bool)saveFileDialog.ShowDialog())
            {
                File.WriteAllBytes(saveFileDialog.FileName, Properties.Resources.Guide);
            }
        }
        
        #endregion
    }
}
