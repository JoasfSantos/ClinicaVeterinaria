namespace ClinicaVet.Model
{
    public enum StatusAgendamento
    {
        Agendado,
        EmAndamento,
        Concluido,
        Cancelado
    }

    public static class StatusAgendamentoExtensions
    {
        public static string ToString(this StatusAgendamento status)
        {
            switch (status)
            {
                case StatusAgendamento.Agendado:
                    return "Agendado";
                case StatusAgendamento.EmAndamento:
                    return "Em Andamento";
                case StatusAgendamento.Concluido:
                    return "Concluído";
                case StatusAgendamento.Cancelado:
                    return "Cancelado";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}