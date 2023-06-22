using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations;

public class CostomerCartConfiguration : IEntityTypeConfiguration<CostumerCart>
{
    public void Configure(EntityTypeBuilder<CostumerCart> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Total)
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Discount)
            .HasColumnType("decimal(18,2)")
            .IsRequired();
    }
}
