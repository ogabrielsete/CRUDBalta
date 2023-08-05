using System.ComponentModel.DataAnnotations;

namespace TesteBalta.Models.ViewModels
{
    public class AlunoViewModel
    {
        
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Sobrenome { get; set; }
        [Required]
        public string CPF { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string Telefone { get; set; }
        public IFormFile ImagemAluno { get; set; }

       
    }
}
