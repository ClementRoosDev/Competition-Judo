using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class PouleDe6 : Poule
    {
        public PouleDe6(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
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
            var c5 = new Coordonnee(140, 150);
            var c6 = new Coordonnee(160, 150);
            listeCoordonneesNom.Add(c1);
            listeCoordonneesNom.Add(c2);
            listeCoordonneesNom.Add(c3);
            listeCoordonneesNom.Add(c4);
            listeCoordonneesNom.Add(c5);
            listeCoordonneesNom.Add(c6);
        }
    }
}
