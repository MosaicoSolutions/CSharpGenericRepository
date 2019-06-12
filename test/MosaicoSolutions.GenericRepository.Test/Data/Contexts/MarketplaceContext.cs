using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.EntityConfigurations;

namespace MosaicoSolutions.GenericRepository.Test.Data.Contexts
{
    public class MarketplaceContext : LogWriteDbContext
    {
        public static MarketplaceContext SqlServerExpress()
            => SqlServer(@"Server=.\SQLEXPRESS;Database=MarketplaceContext;Trusted_Connection=True;");

        public static MarketplaceContext SqlServer(string connectionString)
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseSqlServer(connectionString)
                .Options;

            var bookStoreContext = new MarketplaceContext(options);
            return bookStoreContext;
        }

        public MarketplaceContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
