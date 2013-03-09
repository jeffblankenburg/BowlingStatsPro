using BowlingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BowlingScorer
{
    class PlayerToTotalGamesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Player p = value as Player;
            return p.GameHistory.Count;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
