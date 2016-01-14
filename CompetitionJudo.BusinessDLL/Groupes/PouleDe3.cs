using CompetitionJudo.BusinessDLL.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Business
{
    public class PouleDe3 : Poule
    {
        public PouleDe3(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            this.sourceImage = Resources.templateP3;
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
            var n1 = new Coordonnee(18, 205);
            var n2 = new Coordonnee(18, 260);
            var n3 = new Coordonnee(18, 315);
            listeCoordonneesNom.Add(n1);
            listeCoordonneesNom.Add(n2);
            listeCoordonneesNom.Add(n3);
            var p1 = new Coordonnee(118, 205);
            var p2 = new Coordonnee(118, 260);
            var p3 = new Coordonnee(118, 315);
            listeCoordonneesPrenom.Add(p1);
            listeCoordonneesPrenom.Add(p2);
            listeCoordonneesPrenom.Add(p3);
            var c1 = new Coordonnee(207, 205);
            var c2 = new Coordonnee(207, 260);
            var c3 = new Coordonnee(207, 315);
            listeCoordonneesClub.Add(c1);
            listeCoordonneesClub.Add(c2);
            listeCoordonneesClub.Add(c3);
        }
        
    }
}
