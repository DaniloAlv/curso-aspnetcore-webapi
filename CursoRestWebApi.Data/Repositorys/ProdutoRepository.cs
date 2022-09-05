using CursoRestWebApi.Business.Interfaces.Repositorys;
using CursoRestWebApi.Business.Models;
using CursoRestWebApi.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CursoRestWebApi.Data.Repositorys
{
    public class ProdutoRepository : BaseRepository<Produto>, IProdutoRepository
    {
        public ProdutoRepository(CursoRestWebApiDbContext apiDbContext) : base(apiDbContext)
        {}

        public async Task<Produto> GetProdutoFornecedor(Guid id)
        {
            return await _apiDbContext.Produtos.AsNoTracking()
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<Produto>> GetProdutosByFornecedor(Guid fornecedorId)
        {
            return await _apiDbContext.Produtos.AsNoTracking()
                .Include(p => p.Fornecedor)
                .Where(p => p.FornecedorId.Equals(fornecedorId))
                .ToListAsync();
        }

        public async Task<IEnumerable<Produto>> GetProdutosFornecedor()
        {
            return await _apiDbContext.Produtos.AsNoTracking()
                .Include(p => p.Fornecedor)
                .ToListAsync();
        }
    }
}
