using Book_Shop.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Book_Shop.Data.Mappings
{
    public class ProviderMapping : IEntityTypeConfiguration<Provider>
    {
        public void Configure(EntityTypeBuilder<Provider> builder)
        {
            builder.HasKey(_ => _.Id);

            builder.Property(_ => _.Name)
                .IsRequired()
                .HasColumnType("varchar(200)");

            builder.Property(_ => _.Document)
                .IsRequired()
                .HasColumnType("varchar(14)");

            builder.ToTable("Providers");

            builder.HasOne(_ => _.Address)
                .WithOne(_ => _.Provider);

            builder.HasMany(_ => _.Products)
                .WithOne(_ => _.Provider)
                .HasForeignKey(_ => _.ProviderId);
        }
    }
}
