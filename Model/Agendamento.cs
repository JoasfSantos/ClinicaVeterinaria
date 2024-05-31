using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicaVet.Model
{
    public class Agendamento(DateTime dataAgendamento, StatusAgendamento status, string tipoPet, int idTutor, string nomeTutor, int idColaborador)
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DataAgendamento { get; set; } = dataAgendamento;

        [Required]
        public StatusAgendamento Status { get; set; } = status;

        [Required]
        public string TipoPet { get; set; } = tipoPet;

        [Required]
        [ForeignKey("Usuario")]
        public int IdTutor { get; set; } = idTutor;

        [Required]
        public string NomeTutor { get; set; } = nomeTutor;

        //public virtual Usuario Tutor { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int IdColaborador { get; set; } = idColaborador;

        //public virtual Usuario ColaboradorResponsavel { get; set; } = colaboradorResponsavel;
    }
}