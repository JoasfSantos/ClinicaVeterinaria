using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ClinicaVet.Model
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime DataAgendamento { get; set; }

        [Required]
        public StatusAgendamento Status { get; set; }

        [Required]
        public EspeciePet TipoPet { get; set; }

        [Required]
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }

        public virtual Usuario ColaboradorResponsavel { get; set; }
    }
}