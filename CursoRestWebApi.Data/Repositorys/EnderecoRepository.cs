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
    public class EnderecoRepository : BaseRepository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(CursoRestWebApiDbContext apiDbContext) : base(apiDbContext)
        { }

        public async Task<Endereco> GetFornecedorByFornecedor(Guid fornecedorId)
        {
            return await GetByFilter(e => e.FornecedorId.Equals(fornecedorId));
        }
    }
}
