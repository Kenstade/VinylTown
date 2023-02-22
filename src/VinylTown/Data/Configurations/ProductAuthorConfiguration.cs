using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations;

public class ProductAuthorConfiguration : IEntityTypeConfiguration<ProductAuthor>
{
    public void Configure(EntityTypeBuilder<ProductAuthor> builder)
    {
        builder.Property(p => p.Author)
            .HasMaxLength(50)
            .IsRequired();
    }
}
