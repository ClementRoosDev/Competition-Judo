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


namespace CompetitionJudo.UI
{
    public class ImageGroupe
    {

        Groupe groupe;
        public Image imageGroupe;
        public Graphics g;
        Font drawFont = new Font("Arial", 16);
        SolidBrush drawBrush = new SolidBrush(System.Drawing.Color.Yellow);
        public OrganisationCombat poule;


        public ImageGroupe(Groupe groupe)
        {
            this.groupe = groupe;

            init();
            //imageGroupe = System.Drawing.Image.FromFile(poule.sourceImage);
            imageGroupe = new Bitmap(poule.sourceImage);
            
        }

        private void init()
        {
            if (groupe.typeGroupe == TypeGroupe.Tableau)
            {
                if (groupe.competiteurs.Count <= 4)
                {
                    poule = new TableauDe4(groupe.competiteurs);
                }
                if (groupe.competiteurs.Count > 4 && groupe.competiteurs.Count <= 8)
                {
                    poule = new TableauDe8(groupe.competiteurs);
                }
                if (groupe.competiteurs.Count > 8 && groupe.competiteurs.Count <= 16)
                {
                    poule = new TableauDe16(groupe.competiteurs);
                }
                if (groupe.competiteurs.Count > 16 && groupe.competiteurs.Count <= 32)
                {
                    poule = new TableauDe32(groupe.competiteurs);
                }
            }
            else
            {
                switch (groupe.competiteurs.Count())
                {
                    case 2:                        
                         poule = new PouleDe2(groupe.competiteurs);
                         break;
                    case 3:                        
                        poule = new PouleDe3(groupe.competiteurs); 
                        break;
                    case 4:
                        poule = new PouleDe4(groupe.competiteurs); 
                        break;
                    case 5:
                        poule = new PouleDe5(groupe.competiteurs); 
                        break;
                    case 6:
                        poule = new PouleDe6(groupe.competiteurs); 
                        break;
                }
            }
        }

        private void DrawCompetiteurs()
        {
            for (int i = 0; i < this.groupe.competiteurs.Count(); i++)
            {
                
            }
        }
    }
}
