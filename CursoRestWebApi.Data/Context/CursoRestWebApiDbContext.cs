using CursoRestWebApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursoRestWebApi.Data.Context
{
    public class CursoRestWebApiDbContext : DbContext
    {
        public CursoRestWebApiDbContext(DbContextOptions<CursoRestWebApiDbContext> options) : base(options)
        {
        }

        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CursoRestWebApiDbContext).Assembly);

            foreach (var relation in modelBuilder.Model.GetEntityTypes().SelectMany(r => r.GetForeignKeys()))
            {
                relation.DeleteBehavior = DeleteBehavior.ClientSetNull;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}
