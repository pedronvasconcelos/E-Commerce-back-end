using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NSE.Catalog.API.Models;

namespace NSE.Catalog.API.Data.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnType("varchar(270)");

            builder.Property(c => c.Description)
                .IsRequired()
                .HasColumnType("varchar(1000)");


            builder.Property(c => c.Image)
                .IsRequired()
                .HasColumnType("varchar(300)");

            builder.ToTable("Products");
        }
    }
}
