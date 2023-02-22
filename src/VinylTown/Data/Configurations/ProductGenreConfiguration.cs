using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations;

public class ProductGenreConfiguration : IEntityTypeConfiguration<ProductGenre>
{
    public void Configure(EntityTypeBuilder<ProductGenre> builder)
    {
        builder.Property(p => p.Genre)
            .HasMaxLength(30)
            .IsRequired();
    }
}
