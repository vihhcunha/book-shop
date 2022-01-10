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
    public class EnderecoMapping : IEntityTypeConfiguration<Endereco>
    {
        public void Configure(EntityTypeBuilder<Endereco> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Logradouro)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Numero)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(_ => _.Cep)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(_ => _.Complemento)
                .HasColumnType("varchar(250)");

            builder.Property(_ => _.Bairro)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(_ => _.Cidade)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Estado)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
