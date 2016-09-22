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
        private string nom;
        public string Nom
        {
            get
            {
                if (String.IsNullOrEmpty(nom))
                    return nom;
                return nom.First().ToString().ToUpper() + nom.Substring(1);
            }

            set { nom = value; }
        }
        private string prenom;
        public string Prenom
        {
            get
            {
                if (String.IsNullOrEmpty(prenom))
                    return prenom;
                return prenom.First().ToString().ToUpper() + prenom.Substring(1).ToLower();
            }

            set { prenom = value; }
        }
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
        public int Id { get; }

        public Competiteur()
        {
            Resultat = null;
            Poule = null;
            Id = new Guid().GetHashCode();
        }
    }
}
