using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteBalta.Models
{
    [Table(name: "Aluno")]
    public class Aluno
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Adicione nome ao aluno")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "O nome do pagamento deve conter apenas letras.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Adicione sobrennome ao aluno")]
        [RegularExpression(@"^[\p{L}\s]+$", ErrorMessage = "O nome do pagamento deve conter apenas letras.")]
        public string Sobrenome { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string CPF { get; set; }

        [Required(ErrorMessage = "A data de nascimento é obrigatória.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DataNascimento { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }
        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string ImagemAluno { get; set; }


        public List<Assinatura> Assinaturas { get; set; }
    }
}
