using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.Data.EntityConfigurations
{
    public class OrderItemEntityConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(o => o.OrdemItemId);

            builder.HasOne(o => o.Order).WithMany(o => o.OrderItem).HasForeignKey(o => o.OrderId);
            builder.HasOne(o => o.Product).WithMany(o => o.OrderItem).HasForeignKey(o => o.ProductId);
        }
    }
}