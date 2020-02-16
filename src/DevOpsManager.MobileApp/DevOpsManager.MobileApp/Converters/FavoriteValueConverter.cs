using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace DevOpsManager.MobileApp.Converters
{
    class FavoriteValueConverter : ValueConverterBase<bool, bool>
    {
        public override object Convert(bool value, Type targetType, bool parameter, CultureInfo culture)
        {
            if (parameter)
                return value;
            else return true;
        }
    }
}
