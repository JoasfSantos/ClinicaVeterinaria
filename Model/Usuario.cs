using System.ComponentModel.DataAnnotations;


namespace ClinicaVet.Model
{
    public class Usuario(string nome, string email, string senha, bool colaborador)
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nome { get; set; } = nome;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = email;

        [Required]
        public string Senha { get; set; } = senha;

        [Required]
        public bool Colaborador { get; set; } = colaborador;
    }
}