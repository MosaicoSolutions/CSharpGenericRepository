using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Data.EntitiesConfiguration
{
    public class LogEntityConfiguration : IEntityTypeConfiguration<LogEntity>
    {
        public void Configure(EntityTypeBuilder<LogEntity> builder)
        {
            builder.HasKey(x => x.LogEntityId);

            builder.Property(x => x.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(x => x.EntityFullName)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.EntityAssembly)
                   .IsRequired()
                   .HasMaxLength(255);

            builder.Property(x => x.LogActionType)
                   .IsRequired()
                   .HasConversion<string>();

            builder.Property(x => x.OriginalValues)
                   .IsRequired();

            builder.Property(x => x.TransactionId)
                   .IsRequired();
        }
    }
}
