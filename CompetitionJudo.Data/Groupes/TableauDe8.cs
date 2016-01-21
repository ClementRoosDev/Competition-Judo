using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class TableauDe8 : Tableau
    {
        public TableauDe8(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            //this.sourceImage = "D:\\testT8.png";
        }
        public override void CreerFeuille()
        {
        }

        public override void CreerCoordonnees()
        {
            var c1 = new Coordonnee(100, 50);
            var c2 = new Coordonnee(120, 50);
            var c3 = new Coordonnee(140, 50);
            var c4 = new Coordonnee(160, 50);
            var c5 = new Coordonnee(100, 150);
            var c6 = new Coordonnee(120, 150);
            var c7 = new Coordonnee(140, 150);
            var c8 = new Coordonnee(160, 150);
            listeCoordonneesNom.Add(c1);
            listeCoordonneesNom.Add(c2);
            listeCoordonneesNom.Add(c3);
            listeCoordonneesNom.Add(c4);
            listeCoordonneesNom.Add(c5);
            listeCoordonneesNom.Add(c6);
            listeCoordonneesNom.Add(c7);
            listeCoordonneesNom.Add(c8);
        }
        
    }
}
