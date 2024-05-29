using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicaVet.Model
{
    public class Agendamento(DateTime dataAgendamento, StatusAgendamento status, EspeciePet tipoPet, int idTutor, int idColaborador)
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataAgendamento { get; set; } = dataAgendamento;

        [Required]
        public StatusAgendamento Status { get; set; } = status;

        [Required]
        public EspeciePet TipoPet { get; set; } = tipoPet;

        [Required]
        [ForeignKey("Usuario")]
        public int IdTutor { get; set; } = idTutor;

        //public virtual Usuario Tutor { get; set; } = tutor;

        [Required]
        [ForeignKey("Usuario")]
        public int IdColaborador { get; set; } = idColaborador;

        //public virtual Usuario ColaboradorResponsavel { get; set; } = colaboradorResponsavel;
    }
}