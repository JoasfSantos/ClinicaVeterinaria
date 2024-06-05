#nullable disable
using System.Globalization;


namespace ClinicaVet.Utilidades;
public class TipoButtonStatusConverter : IValueConverter // Classe criada para alterar o button de alteração de status do agendamento.
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return (value as string) switch
        {
            "AGENDADO" => "agendado.png",
            "EM ANDAMENTO" => "em_andamento.png",
            _ => "concluido.png",
        };
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}