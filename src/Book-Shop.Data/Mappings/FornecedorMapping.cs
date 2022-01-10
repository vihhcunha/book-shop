using Book_Shop.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_Shop.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Documento)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.ToTable("Fornecedores");

            builder.HasOne(_ => _.Endereco)
                .WithOne(_ => _.Fornecedor);

            builder.HasMany(_ => _.Produtos)
                .WithOne(_ => _.Fornecedor)
                .HasForeignKey(_ => _.FornecedorId);
        }
    }
}
