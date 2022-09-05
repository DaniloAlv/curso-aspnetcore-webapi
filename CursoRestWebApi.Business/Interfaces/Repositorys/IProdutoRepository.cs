using CursoRestWebApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Interfaces.Repositorys
{
    public interface IProdutoRepository : IBaseRepository<Produto>
    {
        Task<IEnumerable<Produto>> GetProdutosByFornecedor(Guid fornecedorId);
        Task<IEnumerable<Produto>> GetProdutosFornecedor();
        Task<Produto> GetProdutoFornecedor(Guid id);
    }
}
