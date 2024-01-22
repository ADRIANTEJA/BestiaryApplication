using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace BestiaryApplication.Common.Converters
{
    internal class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string? imagePath = value as string;

            if (imagePath != null) return File.ReadAllBytes(imagePath);
            return "default";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            byte[]? image = value as byte[];
            if (image != null) return Encoding.UTF8.GetString(image);
            return "default";
        }
    }
}
