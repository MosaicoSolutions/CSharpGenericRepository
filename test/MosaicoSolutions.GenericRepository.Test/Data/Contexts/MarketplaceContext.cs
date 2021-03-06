﻿using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.EntityConfigurations;
using MosaicoSolutions.GenericRepository.Data.Extensions;

namespace MosaicoSolutions.GenericRepository.Test.Data.Contexts
{
    public class MarketplaceContext : WriteDbContext
    {
        public static MarketplaceContext SqlServerExpress()
            => SqlServer(@"Server=.\SQLEXPRESS;Database=MarketplaceContext;Trusted_Connection=True;");
        
        public static MarketplaceContext SqlServerDocker()
            => SqlServer(@"Server=localhost,1433\Calalog=MarketplaceContext;Database=MarketplaceContext;User=sa;Password=yourStrong(!)Password;");

        public static MarketplaceContext SqlServer(string connectionString)
        {
            var options = new DbContextOptionsBuilder<MarketplaceContext>()
                .UseSqlServer(connectionString)
                .UseLogEntitiesOnSaveChanges()
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
            modelBuilder.ApplyConfiguration(new OrderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new OrderItemEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
