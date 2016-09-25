using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using CompetitionJudo.Business;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Windows.Media;
using CompetitionJudo.Data;

namespace CompetitionJudo.UI
{
    public class ImageGroupe
    {

        public Groupe Groupe {get; set; }
        public Image imageGroupe;
        public Graphics Graphic;
        Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Yellow);
        public OrganisationCombat Organisation { get; set; }

        public ImageGroupe(Groupe groupe)
        {
            this.Groupe = groupe;

            init();

            imageGroupe = new Bitmap(Organisation.sourceImage);
            
        }

        private void init()
        {
            if (Groupe.TypeGroupe == TypeGroupe.Tableau)
            {
                if (Groupe.Competiteurs.Count <= 4)
                {
                    Organisation = new TableauDe4(Groupe.Competiteurs);
                }
                if (Groupe.Competiteurs.Count > 4 && Groupe.Competiteurs.Count <= 8)
                {
                    Organisation = new TableauDe8(Groupe.Competiteurs);
                }
                if (Groupe.Competiteurs.Count > 8 && Groupe.Competiteurs.Count <= 16)
                {
                    Organisation = new TableauDe16(Groupe.Competiteurs);
                }
                if (Groupe.Competiteurs.Count > 16 && Groupe.Competiteurs.Count <= 32)
                {
                    Organisation = new TableauDe32(Groupe.Competiteurs);
                }
            }
            else
            {
                switch (Groupe.Competiteurs.Count())
                {
                    case 2:                        
                         Organisation = new PouleDe2(Groupe.Competiteurs);
                         break;
                    case 3:                        
                        Organisation = new PouleDe3(Groupe.Competiteurs); 
                        break;
                    case 4:
                        Organisation = new PouleDe4(Groupe.Competiteurs); 
                        break;
                    case 5:
                        Organisation = new PouleDe5(Groupe.Competiteurs); 
                        break;
                    case 6:
                        Organisation = new PouleDe6(Groupe.Competiteurs); 
                        break;
                }
            }
        }

        private void DrawCompetiteurs()
        {
            for (int i = 0; i < this.Groupe.Competiteurs.Count(); i++)
            {
                
            }
        }
    }
}
