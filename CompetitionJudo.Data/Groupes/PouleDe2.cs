using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using CompetitionJudo.Data.Properties;

namespace CompetitionJudo.Data
{
    public class PouleDe2 : Poule
    {
        public PouleDe2(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            this.sourceImage = Resources.ResizePoule2;
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
            listeCoordonneesNom.Add(n1);
            listeCoordonneesNom.Add(n2);
            var p1 = new Coordonnee(144, 210);
            var p2 = new Coordonnee(144, 270);
            listeCoordonneesPrenom.Add(p1);
            listeCoordonneesPrenom.Add(p2);
            var c1 = new Coordonnee(257, 210);
            var c2 = new Coordonnee(257, 270);
            listeCoordonneesClub.Add(c1);
            listeCoordonneesClub.Add(c2);
        }
    }
}
