using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.DTOs
{
    public class EnderecoDTO
    {
		[Key]
        public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[DisplayName("Fornecedor")]
        public Guid FornecedorId { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Logradouro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(10, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 2)]
		public string Numero { get; set; }

		[StringLength(150, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 5)]
		public string Complemento { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(8, ErrorMessage = "O campo {0} deve ter {1} caracteres.")]
		public string CEP { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Bairro { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Cidade { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Estado { get; set; }

		public FornecedorDTO Fornecedor { get; set; }
	}
}
