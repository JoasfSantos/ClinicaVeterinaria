#nullable disable
using System.Globalization;


namespace ClinicaVet.Utilidades;
public class TipoPetToImagemConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value as string) switch
        {
            "GATO" => "gato.png",
            "CACHORRO" => "cachorro.png",
            _ => "papagaio.png",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}