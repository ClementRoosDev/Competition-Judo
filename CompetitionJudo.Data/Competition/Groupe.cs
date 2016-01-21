﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class Groupe
    {
        public int MaxCompetiteursParGroupe;
        private bool estValide;

        public bool EstValide
        {
            get
            {

                return (Competiteurs.Count > 1 && Competiteurs.Count <= MaxCompetiteursParGroupe);
            }
            set
            {
                estValide = value;
            }
        }

        public List<Competiteur> Competiteurs { get; set;}
        public int id {get;set;}
        // Poule : Type groupe  = 0 : Tableau, type groupe = 1  : poule
        public TypeGroupe typeGroupe;
        public Groupe()
        {
            Competiteurs = new List<Competiteur>();
            typeGroupe = TypeGroupe.Poule;

        }
        public override string ToString()
        {
            return id.ToString();
        }
    }

    public enum TypeGroupe
    {
        Tableau,
        Poule
    }
}
