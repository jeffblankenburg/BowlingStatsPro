﻿using BowlingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BowlingScorer
{
    class PlayerToGutterballTotalConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Player p = value as Player;
            return p.GutterballTotal;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
