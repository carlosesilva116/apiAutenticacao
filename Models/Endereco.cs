using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace apiAutenticacao.Models
{
    
    
        [Table("Usuarios")]
        public class Endereco
    {
            [Key]
            public int Id { get; set; }

            [Required(ErrorMessage = "O Cep é um campo obrigatório")]
            [StringLength(15, MinimumLength = 2, ErrorMessage = "O Cep deve ter entre 2 e 15 caracteres")]
            public string Cep { get; set; } = string.Empty;

            [Required(ErrorMessage = "O Lougradouro é um campo obrigatório")]
            [StringLength(150, ErrorMessage = "O Lougradouro deve ter no máximo 150 caracteres")]
            public string Lougradouro { get; set; } = string.Empty;

            [JsonIgnore]
            [Required(ErrorMessage = "A senha é obrigatória")]
            [StringLength(255, ErrorMessage = "A senha deve ter no máximo 255 caracteres")]
            public string Senha { get; set; } = string.Empty;

            [JsonIgnore]
            public DateTime DataCadastro { get; set; }
            [JsonIgnore]
            public bool Ativo { get; set; }


            public Endereco()
            {

                DataCadastro = DateTime.Now;

                Ativo = true;


            }

        }
    }

