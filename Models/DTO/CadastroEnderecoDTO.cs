using System.ComponentModel.DataAnnotations;

namespace apiAutenticacao.Models.DTO
{
    public class CadastroEnderecoDTO
    {
        [Required(ErrorMessage = "O CEP é obrigatório")]
        [StringLength(15, MinimumLength = 8)]
        public string Cep { get; set; } = string.Empty;


        [Required(ErrorMessage = "O Logradouro é obrigatório")]
        [StringLength(100, MinimumLength = 3)]
        public string Logradouro { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Número é obrigatório")]
        [StringLength(10, MinimumLength = 1)]
        public string Numero { get; set; } = string.Empty;
        
        [StringLength(50)]
        public string Complemento { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Bairro é obrigatório")]
        [StringLength(100, MinimumLength = 2)]
        public string Bairro { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Cidade é obrigatório")]
        [StringLength(100, MinimumLength = 2)]
        public string Cidade { get; set; } = string.Empty;

        [Required(ErrorMessage = "O Estado é obrigatório")]
        [StringLength(100, MinimumLength = 2)]
        public string Estado { get; set; } = string.Empty;

        [Required(ErrorMessage = "O País é obrigatório")]
        [StringLength(100, MinimumLength = 2)]
        public string Pais { get; set; } = string.Empty;

    }
}
