using CursoRestWebApi.Business.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Business.Interfaces.Repositorys
{
    public interface IEnderecoRepository : IBaseRepository<Endereco>
    {
        Task<Endereco> GetFornecedorByFornecedor(Guid fornecedorId);
    }
}
