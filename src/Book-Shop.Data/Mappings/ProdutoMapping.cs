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
    public class ProdutoMapping : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Descricao)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(_ => _.Imagem)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Produtos");
        }
    }
}
