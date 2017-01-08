using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class Poule : OrganisationCombat
    {
        public Poule()
        {
            CoordonneesNomCompetition = new PointF(320, 20);
            CoordonneesPoidsGroupe = new PointF(320, 40);
            CoordonneesTempsCombat = new PointF(20, 20);
            CoordonneesTempsImmobilisation = new PointF(20, 40);
        }

        public override void CreerCoordonnees()
        {
            throw new NotImplementedException();
        }

        public override void CreerFeuille()
        {
            throw new NotImplementedException();
        }
    }
}
