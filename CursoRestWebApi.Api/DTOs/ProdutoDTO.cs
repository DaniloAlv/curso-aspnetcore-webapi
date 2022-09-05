using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CursoRestWebApi.Api.DTOs
{
    public class ProdutoDTO
    {
		[Key]
        public Guid Id { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[DisplayName("Fornecedor")]
		public Guid FornecedorId { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		public string Nome { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		[StringLength(100, ErrorMessage = "O campo {0} deve ter entre {2} e {1} caracteres.", MinimumLength = 3)]
		[DisplayName("Descrição")]
		public string Descricao { get; set; }

		[Required(ErrorMessage = "O campo {0} é obrigatório!")]
		public decimal Valor { get; set; }

		public bool Ativo { get; set; }

		public DateTime DataCadastro { get; set; }

        public string ImagemUpload { get; set; }

        public string Imagem { get; set; }

        public string NomeFornecedor { get; set; }
	}
}
