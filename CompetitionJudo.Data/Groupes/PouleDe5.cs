using CompetitionJudo.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class PouleDe5 : Poule
    {
        public PouleDe5(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            this.sourceImage = Resources.ResizePoule5;
            CreerCoordonnees();
        }

        public override void CreerFeuille()
        {
        }

        public override void CreerCoordonnees()
        {
            listeCoordonneesNom = new List<Coordonnee>();
            listeCoordonneesPrenom = new List<Coordonnee>();
            listeCoordonneesClub = new List<Coordonnee>();
            var n1 = new Coordonnee(48, 210);
            var n2 = new Coordonnee(48, 270);
            var n3 = new Coordonnee(48, 330);
            var n4 = new Coordonnee(48, 390);
            var n5 = new Coordonnee(48, 450);
            listeCoordonneesNom.Add(n1);
            listeCoordonneesNom.Add(n2);
            listeCoordonneesNom.Add(n3);
            listeCoordonneesNom.Add(n4);
            listeCoordonneesNom.Add(n5);
            var p1 = new Coordonnee(144, 210);
            var p2 = new Coordonnee(144, 270);
            var p3 = new Coordonnee(144, 330);
            var p4 = new Coordonnee(144, 390);
            var p5 = new Coordonnee(144, 450);
            listeCoordonneesPrenom.Add(p1);
            listeCoordonneesPrenom.Add(p2);
            listeCoordonneesPrenom.Add(p3);
            listeCoordonneesPrenom.Add(p4);
            listeCoordonneesPrenom.Add(p5);
            var cl1 = new Coordonnee(257, 210);
            var cl2 = new Coordonnee(257, 270);
            var cl3 = new Coordonnee(257, 330);
            var cl4 = new Coordonnee(257, 390);
            var cl5 = new Coordonnee(257, 450);
            listeCoordonneesClub.Add(cl1);
            listeCoordonneesClub.Add(cl2);
            listeCoordonneesClub.Add(cl3);
            listeCoordonneesClub.Add(cl4);
            listeCoordonneesClub.Add(cl5);

        }
    }
}
