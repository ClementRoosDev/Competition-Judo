using CompetitionJudo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionJudo.UI.ViewModel
{
    public class FenetreImpressionViewModel : BaseViewModel
    {
        public string NomCompetition { get; set; }
        public DateTime DateCompetition { get; set; }
        public List<Groupe> LesGroupes { get; set; }
        public int elementsAImprimer { get; set; }

        public FenetreImpressionViewModel()
        {
            
        }
    }
}
