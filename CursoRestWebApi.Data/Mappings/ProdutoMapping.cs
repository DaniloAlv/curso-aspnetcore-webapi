using CursoRestWebApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Data.Mappings
{
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Nome).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Descricao).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FornecedorId).IsRequired();
            builder.Property(p => p.Valor).IsRequired().HasPrecision(6, 2);
            builder.Property(p => p.Imagem).IsRequired(false);
            builder.Property(p => p.Ativo).HasDefaultValue(false);
            builder.Property(p => p.DataCadastro).IsRequired();

            builder.HasOne(p => p.Fornecedor).WithMany(f => f.Produtos).HasForeignKey(p => p.FornecedorId);
        }
    }
}
