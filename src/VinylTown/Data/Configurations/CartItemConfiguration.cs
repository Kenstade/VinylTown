using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations;

public class CartItemConfiguration : IEntityTypeConfiguration<CartItem>
{
    public void Configure(EntityTypeBuilder<CartItem> builder)
    {
        builder.Property(ci => ci.Price)
            .HasColumnType("decimal(18,2)")
            .IsRequired(true);
    }
}
