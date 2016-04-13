using CompetitionJudo.Data;
using System;
using System.Windows.Data;
using System.Windows.Media;
using System.Linq;

namespace CompetitionJudo.UI
{
    public class ColorationColonnesCompetiteurs : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            Competiteur input = value as Competiteur;
            if (input.Poule%2 != 0)
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
            if (groupe!= null)
            {

                if (groupe.Competiteurs.Count > 0)
                {
                    if (groupe.PoidsMax > ((groupe.PoidsMin) + (10 * groupe.PoidsMin / 100)))
                    {
                        return Brushes.Yellow;
                    }
                    if (groupe.Competiteurs.Select(c=>c.Club).Distinct().Count()!= groupe.Competiteurs.Count())
                    {
                        return Brushes.Yellow;
                    }
                }
            }
            
            
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
