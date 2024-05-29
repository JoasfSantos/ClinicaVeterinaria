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
                    return "AGENDADO";
                case StatusAgendamento.EmAndamento:
                    return "EM ANDAMENTO";
                case StatusAgendamento.Concluido:
                    return "CONCLUÍDO";
                case StatusAgendamento.Cancelado:
                    return "CANCELADO";
                default:
                    throw new ArgumentOutOfRangeException(nameof(status), status, null);
            }
        }
    }
}