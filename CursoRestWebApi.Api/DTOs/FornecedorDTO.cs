using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.DTOs
{
    public class FornecedorDTO
    {
		[Key]
        public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(50, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 3)]
        public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres.", MinimumLength = 11)]
		public string Documento { get; set; }

		public bool Ativo { get; set; }

		public EnderecoDTO Endereco { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		public int TipoFornecedor { get; set; }

		public IEnumerable<ProdutoDTO> Produtos { get; set; }
	}
}
