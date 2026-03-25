using System.Globalization;

namespace ProfitOrder.Controls
{
    class StringToColorConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            string valueAsString = value?.ToString() ?? string.Empty;
            switch (valueAsString)
            {
                case (""): { return Color.FromRgb(0, 0, 0); } // Use black as a default color
                case ("Accent"): { return Color.FromArgb("#0078D7"); }
                default: { return Color.FromArgb(valueAsString); }
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
