using DevOpsManager.MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;

namespace DevOpsManager.MobileApp.Converters
{
    class FavourableValueConverter : ValueConverterBase<ObservableCollection<IFavourable>, bool>
    {
        public override object Convert(ObservableCollection<IFavourable> value, Type targetType, bool parameter, CultureInfo culture)
        {
            if (parameter)
                return new ObservableCollection<IFavourable>(value.Where(v => v.IsFavorite));
            else
                return value;
        }
    }
}
