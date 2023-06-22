using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using VinylTown.Domain;

namespace VinylTown.Data.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(ci => ci.Price)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
