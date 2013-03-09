using BowlingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BowlingScorer
{
    class PlayerToPinsTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Player p = value as Player;
            int PinCount = 0;
            for (int i = 0; i < p.GameHistory.Count; i++)
            {
                PinCount += p.GameHistory[i].Score;
            }
            return PinCount;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}

