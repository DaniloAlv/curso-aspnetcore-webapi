using CursoRestWebApi.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoRestWebApi.Data.Mappings
{
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.FornecedorId).IsRequired();
            builder.Property(e => e.Logradouro).IsRequired().HasMaxLength(100);
            builder.Property(e => e.CEP).IsRequired().IsFixedLength().HasMaxLength(8);
            builder.Property(e => e.Estado).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Cidade).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Bairro).IsRequired().HasMaxLength(50);
            builder.Property(e => e.Complemento).IsRequired(false).HasMaxLength(150);
            builder.Property(e => e.Numero).IsRequired().HasMaxLength(10);

            builder.HasOne(e => e.Fornecedor).WithOne(f => f.Endereco);
        }
    }
}
