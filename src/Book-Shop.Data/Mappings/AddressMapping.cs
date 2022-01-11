using Book_Shop.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_Shop.Data.Mappings
{
    public class AddressMapping : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Street)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Number)
                .IsRequired()
                .HasColumnType("varchar(50)");

            builder.Property(_ => _.ZipCode)
                .IsRequired()
                .HasColumnType("varchar(8)");

            builder.Property(_ => _.Complement)
                .HasColumnType("varchar(250)");

            builder.Property(_ => _.District)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(_ => _.City)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.State)
                .IsRequired()
                .HasColumnType("varchar(50)");
        }
    }
}
