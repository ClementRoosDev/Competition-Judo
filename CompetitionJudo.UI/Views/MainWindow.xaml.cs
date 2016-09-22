using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Serialization;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using CompetitionJudo.Business;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading;
using CompetitionJudo.Business.Serialisation;
using CompetitionJudo.Data.Donnees;
using System.Threading.Tasks;
using CompetitionJudo.Data;
using CompetitionJudo.Business.CompetitionManager;
using CompetitionJudo.UI.Properties;
using CompetitionJudo.UI.ViewModel;

namespace CompetitionJudo.UI
{
    public partial class MainWindow : Window
    {
        private MainWindowViewModel VM;
        
        public MainWindow()
        {
            InitializeComponent();
            VM = new MainWindowViewModel();
            this.DataContext = VM;      
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {        
            var fenetreNewCompetition = new FenetreCompetition(new Donnee());           
            fenetreNewCompetition.Show();
            this.Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox) sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void TextBox_GotFocus_1(object sender, RoutedEventArgs e)
        {
            var tb = (TextBox) sender;
            tb.Text = string.Empty ;
            tb.GotFocus -= TextBox_GotFocus_1;
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
            dialog.FileName = string.Format("{0} {1:MM-dd-yyyy} {2}", VM.NomNouvelleCompetition, VM.DateNouvelleCompetition,VM.LieuNouvelleCompetition);

            if ((bool)dialog.ShowDialog())
            {
                VM.IsLoading = true;

                CompetitionManager CM = new CompetitionManager();
                var donneesNouvelleCompetition = CM.CreerNouvelleCompetition(VM.LieuNouvelleCompetition,VM.NomNouvelleCompetition, VM.DateNouvelleCompetition,dialog.FileName);

                var fenetreCompetition = new FenetreCompetition(donneesNouvelleCompetition);
                fenetreCompetition.Show();

                Close();                  
            }
        }        

        
    }
}
