using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Business.Models
{
    public class Fornecedor : Entity
    {
		public string Nome { get; set; }
		public string Documento { get; set; }
		public bool Ativo { get; set; }
		public Endereco Endereco { get; set; }
		public int TipoFornecedor { get; set; }
        public IEnumerable<Produto> Produtos { get; set; }
	}
}
