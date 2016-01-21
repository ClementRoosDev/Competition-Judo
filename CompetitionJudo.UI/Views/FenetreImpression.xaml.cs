using CompetitionJudo.Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Shapes;
using System.Drawing.Printing;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using CompetitionJudo.Data;
using CompetitionJudo.Data.Donnees;

namespace CompetitionJudo.UI
{

    public partial class FenetreImpression : Window
    {
        List<Competiteur> actualSelected = new List<Competiteur>();
        List<Groupe> lesGroupes = new List<Groupe>();
        int elementsAImprimer;
        Donnee donnees = new Donnee();

        Font drawFont = new Font("Arial", 9);
        SolidBrush drawBrush = new SolidBrush(Color.Black);
        

        public FenetreImpression(List<Groupe> lesGroupes, Donnee donnees)
        {
            this.lesGroupes = lesGroupes;
            this.donnees = donnees;
            lesGroupes.Sort((a, b) => String.Compare(a.id.ToString(), b.id.ToString()));
            elementsAImprimer = lesGroupes.Count();
            
            InitializeComponent();
            actualSelected = lesGroupes.ElementAt(0).Competiteurs;
            ComboBoxListeGroupes.SelectedValue = lesGroupes.ElementAt(0);
            ComboBoxListeGroupes.DataContext = lesGroupes;
            tableauCompetiteurs.DataContext = actualSelected;

            
        }

        private void ListeGroupes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var comboBox = sender as System.Windows.Controls.ComboBox;

            foreach (var groupe in lesGroupes)
            {
                if (groupe.id.ToString().Equals(comboBox.SelectedItem.ToString()))
                {
                    actualSelected  = groupe.Competiteurs;
                    tableauCompetiteurs.DataContext = actualSelected;
                    if (groupe.typeGroupe == TypeGroupe.Poule)
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
            foreach (var groupe in lesGroupes)
            {
                if (groupe.id.ToString().Equals(ComboBoxListeGroupes.Text))
                {
                    if (groupe.typeGroupe == TypeGroupe.Poule)
                    {
                        estUnTableau();
                        groupe.typeGroupe = TypeGroupe.Tableau;
                    }
                    else
                    {
                        estUnePoule();
                        groupe.typeGroupe = TypeGroupe.Poule;
                    }
                }
            }
        }

        private void BoutonImprimerUnSeulGroupe_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPageSoloPage);
            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            printPreview.Document = pd;
            //printPreview.Show();
            pd.Print();
        }

        private void pd_PrintPageSoloPage(object sender, PrintPageEventArgs ev)
        {
            
            Graphics g = ev.Graphics;

            //var ig = new ImageGroupe(lesGroupes.ElementAt(Convert.ToInt16(ComboBoxListeGroupes.Text)-1));
            var ig = new ImageGroupe( lesGroupes.First(c => c.id.ToString().Equals(ComboBoxListeGroupes.Text)));
            var poidsMin = ig.poule.grilleCompetiteurs.Min(c => c.Poids);
            var poidsMax = ig.poule.grilleCompetiteurs.Max(c => c.Poids);

            System.Drawing.Point ulCorner = new System.Drawing.Point(1, 1);
            g.DrawImage(ig.imageGroupe, ulCorner);

            g.DrawString(string.Format("{0} - {1}",donnees.NomCompetition.ToString(),String.Format("{0:d MMMM yyyy}", donnees.DateCompetition)), drawFont, drawBrush, new PointF(320, 20));
                               

            g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig.poule.grilleCompetiteurs[0].Categorie, ig.poule.grilleCompetiteurs[0].Poule.ToString(), poidsMin, poidsMax), drawFont, drawBrush, new PointF(320, 40));
            

            
            for (int i = 0; i < ig.poule.grilleCompetiteurs.Count; i++)
            {
                var cdn = ig.poule.listeCoordonneesNom[i];
                g.DrawString(ig.poule.grilleCompetiteurs[i].Nom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.poule.listeCoordonneesPrenom[i];
                g.DrawString(ig.poule.grilleCompetiteurs[i].Prenom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                cdn = ig.poule.listeCoordonneesClub[i];
                g.DrawString(ig.poule.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
            }
        }



        public void pd_PrintPage(object sender, PrintPageEventArgs ev)
        {
                
                Graphics g = ev.Graphics;

                
                var ig = new ImageGroupe(lesGroupes.ElementAt(lesGroupes.Count() - elementsAImprimer));
                var poidsMin = ig.poule.grilleCompetiteurs.Min(c => c.Poids);
                var poidsMax = ig.poule.grilleCompetiteurs.Max(c => c.Poids);

                System.Drawing.Point ulCorner = new System.Drawing.Point(1, 1);
                g.DrawImage(ig.imageGroupe, ulCorner);



                g.DrawString(string.Format("{0} - {1}", donnees.NomCompetition.ToString(), String.Format("{0:d MMMM yyyy}", donnees.DateCompetition)), drawFont, drawBrush, new PointF(320, 20));


                g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig.poule.grilleCompetiteurs[0].Categorie, ig.poule.grilleCompetiteurs[0].Poule.ToString(), poidsMin, poidsMax), drawFont, drawBrush, new PointF(320, 40));
            

                for (int i = 0; i < ig.poule.grilleCompetiteurs.Count; i++)
                {
                    var cdn = ig.poule.listeCoordonneesNom[i];
                    g.DrawString(ig.poule.grilleCompetiteurs[i].Nom, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                    cdn = ig.poule.listeCoordonneesPrenom[i];
                    g.DrawString(ig.poule.grilleCompetiteurs[i].Prenom, drawFont, drawBrush,new PointF(cdn.x, cdn.y));
                    cdn = ig.poule.listeCoordonneesClub[i];
                    g.DrawString(ig.poule.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y));
                }

                if (!(elementsAImprimer == 1 && lesGroupes.Count()%2==1))
                {
                    var ig2 = new ImageGroupe(lesGroupes.ElementAt(lesGroupes.Count() - elementsAImprimer+1));
                    var poidsMin2 = ig2.poule.grilleCompetiteurs.Min(c => c.Poids);
                    var poidsMax2 = ig2.poule.grilleCompetiteurs.Max(c => c.Poids);

                    var ulCorner2 = new System.Drawing.Point(1, 585);
                    g.DrawImage(ig2.imageGroupe, ulCorner2);

                    g.DrawString(string.Format("{0} - {1}", donnees.NomCompetition.ToString(), String.Format("{0:d MMMM yyyy}", donnees.DateCompetition)), drawFont, drawBrush, new PointF(320, 20+585));


                    g.DrawString(string.Format("{0} : poule n°{1} de {2}kg à {3}kg", ig2.poule.grilleCompetiteurs[0].Categorie, ig2.poule.grilleCompetiteurs[0].Poule.ToString(), poidsMin, poidsMax), drawFont, drawBrush, new PointF(320, 40+585));
            
                
                    g.DrawString(String.Format("{0:d MMMM yyyy}", donnees.DateCompetition), drawFont, drawBrush, new PointF(320, 60 + 585));

                    for (int i = 0; i < ig2.poule.grilleCompetiteurs.Count; i++)
                    {
                        var cdn = ig2.poule.listeCoordonneesNom[i];
                        g.DrawString(ig2.poule.grilleCompetiteurs[i].Nom , drawFont, drawBrush, new PointF(cdn.x, cdn.y+585));
                        cdn = ig2.poule.listeCoordonneesPrenom[i];
                        g.DrawString(ig2.poule.grilleCompetiteurs[i].Prenom , drawFont, drawBrush, new PointF(cdn.x, cdn.y + 585));
                        cdn = ig2.poule.listeCoordonneesClub[i];
                        g.DrawString(ig2.poule.grilleCompetiteurs[i].Club, drawFont, drawBrush, new PointF(cdn.x, cdn.y + 585));                      
                    }
                }

                if (elementsAImprimer <= 2)
                    ev.HasMorePages = false;
                else
                {
                    ev.HasMorePages = true;
                    elementsAImprimer -= 2;
                }            
        }

        private void BoutonImprimerTousLesGroupes_Click(object sender, RoutedEventArgs e)
        {
            
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            PrintPreviewDialog printPreview = new PrintPreviewDialog();
            printPreview.Document = pd;
            //printPreview.Show();
            pd.Print();
        }

        private void GenererNoms()
        {

        }
    }
}
