using BowlingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BowlingScorer
{
    class PlayerToAverageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Player p = value as Player;
            int PinTotal = 0;
            for (int i = 0; i < p.GameHistory.Count; i++)
            {
                PinTotal += p.GameHistory[i].Score;
            }
            if (p.GameHistory.Count == 0) return "-";
            else return (PinTotal / p.GameHistory.Count).ToString("N2");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
