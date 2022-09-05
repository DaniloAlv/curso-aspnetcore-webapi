using CursoRestWebApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome).IsRequired().HasMaxLength(50);
            builder.Property(c => c.Documento).IsRequired().HasMaxLength(14);
            builder.Property(c => c.Ativo).HasDefaultValue(false);
            builder.Property(c => c.TipoFornecedor).IsRequired();

            builder.HasOne(c => c.Endereco).WithOne(c => c.Fornecedor);
            builder.HasMany(c => c.Produtos).WithOne(c => c.Fornecedor);
        }
    }
}
