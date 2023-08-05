using System.ComponentModel.DataAnnotations;

namespace TesteBalta.Models.ViewModels
{
    public class AssinaturaViewModel
    {
        public int Id { get; set; }
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public DateTime Inicio { get; set; } = DateTime.Now;
        public DateTime Termino { get; set; }
        public bool Ativo { get; set; }

        [Required]
        public int AlunoId { get; set; }
    }
}
