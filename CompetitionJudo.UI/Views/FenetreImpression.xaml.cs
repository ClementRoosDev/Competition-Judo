using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Drawing.Printing;
using System.Drawing;
using CompetitionJudo.Data;
using CompetitionJudo.UI.ViewModel;

namespace CompetitionJudo.UI
{

    public partial class FenetreImpression : Window
    {
        #region Private Properties

        FenetreImpressionViewModel VM;
        List<Competiteur> actualSelected = new List<Competiteur>();

        #endregion

        #region Style

        private Font drawFont = new Font("Arial", 9);
        private SolidBrush drawBrush = new SolidBrush(Color.Black);

        #endregion

        public FenetreImpression(List<Groupe> lesGroupes, string nomCompetition, DateTime dateCompetition)
        {
            InitializeComponent();

            VM = new FenetreImpressionViewModel();
            this.DataContext = VM;
            VM.DateCompetition = dateCompetition;
            VM.NomCompetition = nomCompetition;
            VM.LesGroupes = lesGroupes;
            VM.ElementsAImprimer = VM.LesGroupes.Count();

            lesGroupes.Sort((a, b) => String.Compare(a.id.ToString(), b.id.ToString()));

            actualSelected = lesGroupes.ElementAt(0).Competiteurs;
            ComboBoxListeGroupes.SelectedValue = lesGroupes.ElementAt(0);
            ComboBoxListeGroupes.DataContext = lesGroupes;
            tableauCompetiteurs.DataContext = actualSelected;
        }

        private void ListeGroupes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;

            foreach (var groupe in VM.LesGroupes)
            {
                if (groupe.id.ToString().Equals(comboBox.SelectedItem.ToString()))
                {
                    actualSelected = groupe.Competiteurs;
                    tableauCompetiteurs.DataContext = actualSelected;
                    if (groupe.TypeGroupe == TypeGroupe.Poule)
                    {
                        estUnePoule();
                    }
                    else
                    {
                        estUnTableau();
                    }
                }
            }
        }

        private void estUnePoule()
        {
            LabelTableau.TextDecorations = TextDecorations.Strikethrough;
            LabelPoule.TextDecorations = null;
        }

        private void estUnTableau()
        {
            LabelPoule.TextDecorations = TextDecorations.Strikethrough;
            LabelTableau.TextDecorations = null;
        }

        private void BoutonPouleToTableau_Click(object sender, RoutedEventArgs e)
        {
            foreach (var groupe in VM.LesGroupes)
            {
                if (groupe.id.ToString().Equals(ComboBoxListeGroupes.Text))
                {
                    if (groupe.TypeGroupe == TypeGroupe.Poule)
                    {
                        estUnTableau();
                        groupe.TypeGroupe = TypeGroupe.Tableau;
                    }
                    else
                    {
                        estUnePoule();
                        groupe.TypeGroupe = TypeGroupe.Poule;
                    }
                }
            }
        }

        private void pd_PrintPageSoloPage(object sender, PrintPageEventArgs ev)
        {
            Graphics g = ev.Graphics;

            var ig = new ImageGroupe(VM.LesGroupes.First(c => c.id.ToString().Equals(ComboBoxListeGroupes.Text)));
            var poidsMin = ig.Organisation.grilleCompetiteurs.Min(c => c.Poids);
            var poidsMax = ig.Organisation.grilleCompetiteurs.Max(c => c.Poids);

            System.Drawing.Point ulCorner = new System.Drawing.Point(1, 1);
            g.DrawImage(ig.imageGroupe, ulCorner);

            g.DrawString(string.Format("{0} - {1}", VM.NomCompetition.ToString(), String.Format("{0:d MMMM yyyy}", VM.DateCompetition)), drawFont, drawBrush, ig.Organisation.CoordonneesNomCompetition);

            g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig.Groupe.Categorie, ig.Organisation.grilleCompetiteurs[0].Poule.ToString(), poidsMin, poidsMax), drawFont, drawBrush, ig.Organisation.CoordonneesPoidsGroupe);

            g.DrawString(string.Format("Temps Combat : {0}m{1}s", ig.Groupe.TempsCombat.TimeSinceLastEvent.Minutes, ig.Groupe.TempsCombat.TimeSinceLastEvent.Seconds), drawFont, drawBrush, ig.Organisation.CoordonneesTempsCombat);
            g.DrawString(string.Format("Temps Immobilisation Ippon : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds), drawFont, drawBrush, new PointF(20, 60));
            g.DrawString(string.Format("Temps Immobilisation Waza Ari : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 5), drawFont, drawBrush, new PointF(20, 80));
            g.DrawString(string.Format("Temps Immobilisation Yuko : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 10), drawFont, drawBrush, new PointF(20, 100));

            for (int i = 0; i < ig.Organisation.grilleCompetiteurs.Count; i++)
            {
                var cdn = ig.Organisation.listeCoordonneesNom[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Nom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.Organisation.listeCoordonneesPrenom[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Prenom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.Organisation.listeCoordonneesClub[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
            }
        }

        public void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
            Graphics g = ev.Graphics;

            var ig = new ImageGroupe(VM.LesGroupes.ElementAt(VM.LesGroupes.Count() - VM.ElementsAImprimer));
            var poidsMinG1 = ig.Organisation.grilleCompetiteurs.Min(c => c.Poids);
            var poidsMaxG1 = ig.Organisation.grilleCompetiteurs.Max(c => c.Poids);

            System.Drawing.Point ulCorner = new System.Drawing.Point(1, 1);
            g.DrawImage(ig.imageGroupe, ulCorner);

            g.DrawString(string.Format("{0} - {1}", VM.NomCompetition.ToString(), String.Format("{0:d MMMM yyyy}", VM.DateCompetition)), drawFont, drawBrush, new PointF(320, 20));

            g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig.Groupe.Categorie, ig.Organisation.grilleCompetiteurs[0].Poule.ToString(), poidsMinG1, poidsMaxG1), drawFont, drawBrush, new PointF(320, 40));

            g.DrawString(string.Format("Temps Combat : {0}m{1}s", ig.Groupe.TempsCombat.TimeSinceLastEvent.Minutes, ig.Groupe.TempsCombat.TimeSinceLastEvent.Seconds), drawFont, drawBrush, new PointF(20, 40));
            g.DrawString(string.Format("Temps Immobilisation Ippon : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds), drawFont, drawBrush, new PointF(20, 60));
            g.DrawString(string.Format("Temps Immobilisation Waza ari : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 5), drawFont, drawBrush, new PointF(20, 80));
            g.DrawString(string.Format("Temps Immobilisation Yuko : {0}s", ig.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 10), drawFont, drawBrush, new PointF(20, 100));

            for (int i = 0; i < ig.Organisation.grilleCompetiteurs.Count; i++)
            {
                var cdn = ig.Organisation.listeCoordonneesNom[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Nom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.Organisation.listeCoordonneesPrenom[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Prenom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.Organisation.listeCoordonneesClub[i];
                g.DrawString(ig.Organisation.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
            }

            if (!(VM.ElementsAImprimer == 1 && VM.LesGroupes.Count() % 2 == 1))
            {
                var ig2 = new ImageGroupe(VM.LesGroupes.ElementAt(VM.LesGroupes.Count() - VM.ElementsAImprimer + 1));
                var poidsMinG2 = ig2.Organisation.grilleCompetiteurs.Min(c => c.Poids);
                var poidsMaxG2 = ig2.Organisation.grilleCompetiteurs.Max(c => c.Poids);

                var ulCorner2 = new System.Drawing.Point(1, 585);
                g.DrawImage(ig2.imageGroupe, ulCorner2);

                g.DrawString(string.Format("{0} - {1}", VM.NomCompetition.ToString(), String.Format("{0:d MMMM yyyy}", VM.DateCompetition)), drawFont, drawBrush, new PointF(320, 20 + 585));

                g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig2.Groupe.Categorie, ig2.Organisation.grilleCompetiteurs[0].Poule.ToString(), poidsMinG2, poidsMaxG2), drawFont, drawBrush, new PointF(320, 40 + 585));

                g.DrawString(string.Format("Temps Combat : {0}m{1}s", ig2.Groupe.TempsCombat.TimeSinceLastEvent.Minutes, ig2.Groupe.TempsCombat.TimeSinceLastEvent.Seconds), drawFont, drawBrush, new PointF(20, 40 + 585));
                g.DrawString(string.Format("Temps Immobilisation Ippon : {0}s", ig2.Groupe.TempsImmo.TimeSinceLastEvent.Seconds), drawFont, drawBrush, new PointF(20, 60 + 585));
                g.DrawString(string.Format("Temps Immobilisation Waza Ari : {0}s", ig2.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 5), drawFont, drawBrush, new PointF(20, 80 + 585));
                g.DrawString(string.Format("Temps Immobilisation Yuko : {0}s", ig2.Groupe.TempsImmo.TimeSinceLastEvent.Seconds - 10), drawFont, drawBrush, new PointF(20, 100 + 585));

                for (int i = 0; i < ig2.Organisation.grilleCompetiteurs.Count; i++)
                {
                    var cdn = ig2.Organisation.listeCoordonneesNom[i];
                    g.DrawString(ig2.Organisation.grilleCompetiteurs[i].Nom, drawFont, drawBrush, new PointF(cdn.x, cdn.y + 585));
                    cdn = ig2.Organisation.listeCoordonneesPrenom[i];
                    g.DrawString(ig2.Organisation.grilleCompetiteurs[i].Prenom, drawFont, drawBrush, new PointF(cdn.x, cdn.y + 585));
                    cdn = ig2.Organisation.listeCoordonneesClub[i];
                    g.DrawString(ig2.Organisation.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y + 585));
                }
            }

            if (VM.ElementsAImprimer <= 2)
            {
                ev.HasMorePages = false;
            }
            else
            {
                ev.HasMorePages = true;
                VM.ElementsAImprimer -= 2;
            }
        }

        private void BoutonImprimerUnSeulGroupe_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPageSoloPage);
            //PrintPreviewDialog printPreview = new PrintPreviewDialog();
            //printPreview.Document = pd;
            //printPreview.Show();
            pd.Print();
        }

        private void BoutonImprimerTousLesGroupes_Click(object sender, RoutedEventArgs e)
        {

            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            //PrintPreviewDialog printPreview = new PrintPreviewDialog();
            //printPreview.Document = pd;
            //printPreview.Show();
            pd.Print();
            this.Close();
        }
    }
}
