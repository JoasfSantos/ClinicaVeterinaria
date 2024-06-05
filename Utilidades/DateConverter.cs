#nullable disable
using System.Globalization;


namespace ClinicaVet.Utilidades; // Classe criada para alterar o formato da data para o padrão BR.
public class DateConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date.ToString("dd/MM/yyyy");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}