using CompetitionJudo.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class TableauDe4 : Tableau
    {
        public TableauDe4(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            this.sourceImage = Resources.TemplateT4;
            CreerCoordonnees();
        }

        public override void CreerFeuille()
        {
        }

        public override void CreerCoordonnees()
        {
            listeCoordonneesClub = new List<Coordonnee>();
            listeCoordonneesNom = new List<Coordonnee>();
            listeCoordonneesPrenom = new List<Coordonnee>();

            var n1 = new Coordonnee(100, 50);
            var n2 = new Coordonnee(120, 50);
            var n3 = new Coordonnee(140, 50);
            var n4 = new Coordonnee(160, 50);
            listeCoordonneesNom.Add(n1);
            listeCoordonneesNom.Add(n2);
            listeCoordonneesNom.Add(n3);
            listeCoordonneesNom.Add(n4);

            var p1 = new Coordonnee(100, 80);
            var p2 = new Coordonnee(120, 80);
            var p3 = new Coordonnee(140, 80);
            var p4 = new Coordonnee(160, 80);
            listeCoordonneesPrenom.Add(p1);
            listeCoordonneesPrenom.Add(p2);
            listeCoordonneesPrenom.Add(p3);
            listeCoordonneesPrenom.Add(p4);

            var c1 = new Coordonnee(100, 100);
            var c2 = new Coordonnee(120, 100);
            var c3 = new Coordonnee(140, 100);
            var c4 = new Coordonnee(160, 100);
            listeCoordonneesClub.Add(c1);
            listeCoordonneesClub.Add(c2);
            listeCoordonneesClub.Add(c3);
            listeCoordonneesClub.Add(c4);
        }
    }
}
