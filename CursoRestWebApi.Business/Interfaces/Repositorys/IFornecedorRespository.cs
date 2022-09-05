using CursoRestWebApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Interfaces.Repositorys
{
    public interface IFornecedorRespository : IBaseRepository<Fornecedor>
    {
        Task<Fornecedor> GetEnderecoFornecedor(Guid id);
        Task<Fornecedor> GetProdutosFornecedor(Guid id);
    }
}
