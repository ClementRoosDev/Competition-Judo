using CompetitionJudo.Data;
using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;
using System.Globalization;

namespace CompetitionJudo.UI
{
    public class ColorationColonnesCompetiteurs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Competiteur input = value as Competiteur;
            if (input.Poule % 2 != 0)
            {
                return Brushes.LightGray;
            }
            else
            {
                return Brushes.White;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ColorationColonnesGroupes : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Groupe groupe = value as Groupe;

            SolidColorBrush lineColor = Brushes.White;

            if (groupe != null)
            {
                groupe.Commentaire = string.Empty;
                
                if (groupe.CompositionGroupe == Sexes.Mixte)
                {
                    groupe.Commentaire = "Mixte." + groupe.Commentaire;
                    lineColor = Brushes.Yellow;
                }           
                if (groupe.Competiteurs.Count > 1)
                {
                    if (groupe.Competiteurs.Select(c => c.Club).Distinct().Count() != groupe.Competiteurs.Count())
                    {
                        groupe.Commentaire = "+1 même club par poule."+ groupe.Commentaire;
                        lineColor = Brushes.Yellow;
                    }
                    if (groupe.PoidsMax > ((groupe.PoidsMin) + (10 * groupe.PoidsMin / 100)))
                    {
                        if (groupe.PoidsMax > ((groupe.PoidsMin) + (20 * groupe.PoidsMin / 100)))
                        {
                            groupe.Commentaire = "20% différence poids."+ groupe.Commentaire;
                            lineColor = Brushes.Orange;
                        }
                        else
                        {
                            groupe.Commentaire = "10% différence poids."+ groupe.Commentaire;
                            lineColor = Brushes.Yellow;
                        }                        
                    }                    
                }
                if (groupe.Competiteurs.Count <= 1)
                {
                    groupe.Commentaire = "1 seul compétiteur."+ groupe.Commentaire;
                    lineColor = Brushes.OrangeRed;
                }
            }
            return lineColor;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
