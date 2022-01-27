using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using MazeApp.Models;

namespace MazeApp.ViewModel.Converters
{
    public class TextToPositionConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var position = (Position)value;
            if(null == position)
            {
                return string.Empty;
            }

            return $"{position.X},{position.Y}";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var textValue = (string)value;
            if (string.IsNullOrEmpty(textValue))
            {
                return null;
            }
            var coordinates = textValue.Split(',');
            return new Position(int.Parse(coordinates[0]), int.Parse(coordinates[1]));
        }
    }
}
