using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Logique d'interaction pour SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : Window
    {
        MainWindow win;
        public SplashScreen()
        {
            InitializeComponent();
            BackgroundWorker bg = new BackgroundWorker();
            bg.DoWork +=bg_DoWork;
            bg.WorkerReportsProgress = true;
            bg.ProgressChanged+=bg_ProgressChanged;
            bg.RunWorkerCompleted+=bg_RunWorkerCompleted;
            bg.RunWorkerAsync();
        }

        private void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            var worker = sender as BackgroundWorker;
            for (int i = 0; i < 400; i++)
            {
                Thread.Sleep(5);
                worker.ReportProgress(i);
            }
        }

        private void bg_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            stackpanel.Margin = new Thickness(10, 10,400-e.ProgressPercentage, 10);
        }

        private void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            win = new MainWindow();
            win.Show();
            this.Close();
        }
    }
}
