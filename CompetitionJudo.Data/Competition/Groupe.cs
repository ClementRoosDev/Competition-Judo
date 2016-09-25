using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class Groupe
    {
        public int MaxCompetiteursParPoule;
        public TimeSpan2 TempsCombat { get; set; }
        public TimeSpan2 TempsImmo { get; set; }
        public bool PourImpression { get; set; }
        public Categories Categorie { get; set; }
        public string Commentaire { get; set; }
        public bool EstValide
        {
            get
            {
                return (Competiteurs.Count > 1);
            }
            set
            {
                estValide = value;
            }
        }
        public List<Competiteur> Competiteurs { get; set; }
        public int id { get; set; }            
        public TypeGroupe TypeGroupe { get; set; }
        public Groupe()
        {
            Competiteurs = new List<Competiteur>();
            TypeGroupe = TypeGroupe.Poule;
        }

        public override string ToString()
        {
            return id.ToString();
        }

        private bool estValide;

        public double? PoidsMin { get { if (Competiteurs.Count > 0) return Competiteurs.OrderBy(c => c.Poids).First().Poids; else return null; } }
        public double? PoidsMax { get {  if (Competiteurs.Count > 0) return Competiteurs.OrderByDescending(c => c.Poids).First().Poids; else return null; } }
        public Sexes CompositionGroupe { get; set; }        
    }

    public enum TypeGroupe
    {
        Tableau,
        Poule
    }
    
    
}
