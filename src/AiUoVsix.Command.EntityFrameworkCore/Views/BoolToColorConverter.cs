using System;
using Avalonia.Data.Converters;
using System.Globalization;
using Avalonia.Media;

namespace AiUoVsix.Command.EntityFrameworkCore.Views
{
    public class BoolToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
            {
                string colorParam = parameter as string ?? "#10B981,#EF4444";
                string[] colors = colorParam.Split(',');
                
                string colorStr = b ? colors[0] : (colors.Length > 1 ? colors[1] : colors[0]);
                return new SolidColorBrush(Color.Parse(colorStr));
            }
            
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}