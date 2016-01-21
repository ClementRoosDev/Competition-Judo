using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CompetitionJudo.Data
{
    public class Competiteur
    {
        public int? Poule { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Club { get; set; }
        public double Poids { get; set; } 
        public Sexes Sexe { get; set; }
        public Categories Categorie { get; set; }
        public bool EstPresent { get; set; }
        public bool PourImpression { get; set; }
        public int? Resultat { get; set; }
        public Presence Presence
        {
            get
            {
                if (EstPresent)
                {
                    return Presence.Présent;
                }
                else
                {
                    return Presence.NonPrésent;
                }
            }
        }


        public List<Competiteur> listeCompetiteur;

        public Competiteur()
        {
            Resultat = null;
            Poule = null;
        }
    }
}
