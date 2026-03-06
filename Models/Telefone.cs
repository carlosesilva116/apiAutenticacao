using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace apiAutenticacao.Models
{

    [Table("Usuarios")]
    public class Telefone
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Telefone é um campo obrigatório")]
        public string Numero { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O Tipo de telefone é um campo obrigatório")]
        public string Tipo { get; set; } = string.Empty;

    
    }
}
