using System.Globalization;


namespace ClinicaVet.Utilidades;
public class TipoButtonStatusConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        switch (value as string)
        {
            case "AGENDADO":
                return "agendado.png";
            case "EM_ANDAMENTO":
                return "em_andamento.png";
            default:
                return "concluido.png";
        }
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}