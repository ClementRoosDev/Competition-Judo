using CompetitionJudo.Data.Donnees;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace CompetitionJudo.Business.Serialisation
{
    public class DataSerialisation
    {
        public Donnee ReadFile(string FilePath)
        {            
            Donnee donnee;
            var deserial = new XmlSerializer(typeof(Donnee));

            using (var reader = new StreamReader(FilePath) )
            {
                donnee = (Donnee)deserial.Deserialize(reader);
                donnee.CheminFichier = FilePath;
            }            
            
            return donnee;
        }

        public void EnregistrerCompetition(string cheminFichier, Donnee donnees)
        {
            var serialise = new XmlSerializer(typeof(Donnee));
            using (var reader = new StreamWriter(cheminFichier))
            {
                serialise.Serialize(reader, donnees);
            }
        }
    }
}
