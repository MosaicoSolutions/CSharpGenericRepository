using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.Data.EntityConfigurations
{
    public class AuthorEntityConfiguration : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.HasKey(a => a.AuthorId);
            builder.HasMany(a => a.Books).WithOne(b => b.Author).HasForeignKey(b => b.AuthorId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}