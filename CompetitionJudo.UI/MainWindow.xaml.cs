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

namespace CompetitionJudo.UI
{

    public partial class MainWindow : Window
    {

        private Donnee donnee = new Donnee();

        public MainWindow()
        {
            InitializeComponent();         
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

        
        private void OuvrirCompetition_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            if ((bool)dialog.ShowDialog())
            {
                BoutonOuvrirCompetition.IsEnabled = false;
                BoutonNouvelleCompetition.IsEnabled = false;

                BackgroundWorker bgwOpen = new BackgroundWorker();
                bgwOpen.WorkerReportsProgress = true;
                bgwOpen.DoWork += bgwOpen_DoWork;
                bgwOpen.ProgressChanged +=bgwOpen_ProgressChanged;
                bgwOpen.RunWorkerCompleted +=bgwOpen_RunWorkerCompleted;
                bgwOpen.RunWorkerAsync(dialog.FileName);      
            }
        }

        private void bgwOpen_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            worker.ReportProgress(1);
            donnee.cheminFichier = (string)e.Argument ;
            var deserial = new XmlSerializer(typeof(Donnee));
            StreamReader reader = new StreamReader((string)e.Argument);
            donnee = (Donnee)deserial.Deserialize(reader);
            reader.Close();            
        }

        private void bgwOpen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var fenetreCompetition = new FenetreCompetition(donnee);
            fenetreCompetition.Show();
            this.Close();
            ProgressBar.IsIndeterminate = false;
        }

        private void bgwOpen_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.IsIndeterminate = true;
        }
        

        private void NouvelleCompetition_Click_1(object sender, RoutedEventArgs e)
        {
            var dialog = new SaveFileDialog();
            dialog.Filter = "xml files (*.xml)|*.xml";
            dialog.FileName = Nom.Text + " " + String.Format("{0:MM-dd-yyyy}", dpicker.SelectedDate) + " " + Lieu.Text;
            if ((bool)dialog.ShowDialog())
            {
                BoutonOuvrirCompetition.IsEnabled = false;
                BoutonNouvelleCompetition.IsEnabled = false;

                BackgroundWorker bgwNew = new BackgroundWorker();
                bgwNew.WorkerReportsProgress = true;
                bgwNew.DoWork +=bgwNew_DoWork;
                bgwNew.ProgressChanged +=bgwNew_ProgressChanged; ;
                bgwNew.RunWorkerCompleted +=bgwNew_RunWorkerCompleted;
                List<object> arguments = new List<object>();
                arguments.Add(dialog.FileName);
                arguments.Add(dpicker.SelectedDate);
                arguments.Add(Nom.Text);
                arguments.Add(Lieu.Text);
                bgwNew.RunWorkerAsync(arguments);                     
            }
        }        

        private void bgwNew_DoWork(object sender, DoWorkEventArgs e)
        {
            List<object> listArg = e.Argument as List<object>;
            string path = (string)listArg[0];
            DateTime date = (DateTime)listArg[1];
            string nom = (string)listArg[2];
            string lieu = (string)listArg[3];

            BackgroundWorker worker = sender as BackgroundWorker;

            Donnee donneeCompetition = new Donnee()
            {
                listeAllCompetiteurs = new ObservableCollection<Competiteur>(),
                lieuCompetition = lieu,
                nomCompetition = nom,
                dateCompetition = date,
                cheminFichier = path,
                nombreParPoule = 4,
                tempsCombat = new NewDictionary<Categories, TimeSpan2>(createParametres())
            };
            worker.ReportProgress(1);
            e.Result = donneeCompetition;       
        }

        private void bgwNew_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressBar.IsIndeterminate = true;
        }

        private void bgwNew_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           Donnee datas = (Donnee)e.Result as Donnee;
           var fenetreCompetition = new FenetreCompetition(datas);
           fenetreCompetition.Show();
           this.Close();     
        }

        private static Dictionary<Categories, TimeSpan2> createParametres()
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
    }
}
