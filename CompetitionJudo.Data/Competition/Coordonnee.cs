using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CompetitionJudo.Data
{
    public class Coordonnee
    {
        public int x { get; set; }
        public int y { get; set; }

        public Coordonnee(int xx, int yy)
        {
            this.x = xx;
            this.y = yy;
        }
    }
}
