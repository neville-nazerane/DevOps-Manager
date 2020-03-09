using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DevOpsManager.MobileApp.Converters
{
    class FavoriteBtnTextValueConverter : ValueConverterBase<bool>
    {
        public override object Convert(bool value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value)
                return "Show all";
            else
                return "Show only favorites";
        }
    }
}
