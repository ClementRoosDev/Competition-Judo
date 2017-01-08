﻿using CompetitionJudo.Data.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class PouleDe3 : Poule
    {
        public PouleDe3(List<Competiteur> grilleCompetiteurs)
        {
            this.grilleCompetiteurs = grilleCompetiteurs;
            this.sourceImage = Resources.ResizePoule3;
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
            var n1 = new Coordonnee(45, 210);
            var n2 = new Coordonnee(45, 270);
            var n3 = new Coordonnee(45, 330);
            listeCoordonneesNom.Add(n1);
            listeCoordonneesNom.Add(n2);
            listeCoordonneesNom.Add(n3);
            var p1 = new Coordonnee(145, 210);
            var p2 = new Coordonnee(145, 270);
            var p3 = new Coordonnee(145, 330);
            listeCoordonneesPrenom.Add(p1);
            listeCoordonneesPrenom.Add(p2);
            listeCoordonneesPrenom.Add(p3);
            var c1 = new Coordonnee(258, 210);
            var c2 = new Coordonnee(258, 270);
            var c3 = new Coordonnee(258, 330);
            listeCoordonneesClub.Add(c1);
            listeCoordonneesClub.Add(c2);
            listeCoordonneesClub.Add(c3);
        }
        
    }
}
