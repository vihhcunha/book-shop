using Book_Shop.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_Shop.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");

            builder.Property(_ => _.Image)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.ToTable("Products");
        }
    }
}
