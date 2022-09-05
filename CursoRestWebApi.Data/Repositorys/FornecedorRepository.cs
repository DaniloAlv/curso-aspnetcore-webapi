using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Data.Repositorys
{
    public class FornecedorRepository : BaseRepository<Fornecedor>, IFornecedorRespository
    {
        public FornecedorRepository(CursoRestWebApiDbContext apiDbContext) : base(apiDbContext)
        {}

        public async Task<Fornecedor> GetEnderecoFornecedor(Guid id)
        {
            return await _apiDbContext.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .FirstOrDefaultAsync(f => f.Id.Equals(id));
        }

        public async Task<Fornecedor> GetProdutosFornecedor(Guid id)
        {
            return await _apiDbContext.Fornecedores.AsNoTracking()
                .Include(f => f.Endereco)
                .Include(f => f.Produtos)
                .FirstOrDefaultAsync(f => f.Id.Equals(id));
        }
    }
}
