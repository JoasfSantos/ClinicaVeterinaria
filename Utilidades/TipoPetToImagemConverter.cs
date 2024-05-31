using System.Globalization;


namespace ClinicaVet.Utilidades;
public class TipoPetToImagemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value as string)
        {
            case "GATO":
                return "gato.png";
            case "CACHORRO":
                return "cachorro.png";
            default:
                return "papagaio.png";
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}