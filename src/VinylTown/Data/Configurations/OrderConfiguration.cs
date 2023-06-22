using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.CustomerId)
                .IsRequired()
                .HasMaxLength(256);

            builder.Property(p => p.Total)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
        }
    }
}
