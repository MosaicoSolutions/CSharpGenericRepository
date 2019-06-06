using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Contexts;
using System.Reflection;

namespace MosaicoSolutions.GenericRepository.Test.Data.Contexts
{
    public class WriteBookStoreContext : WriteDbContext
    {
        public WriteBookStoreContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public static WriteBookStoreContext SqlServerExpress()
            => SqlServer(@"Server=.\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;");

        public static WriteBookStoreContext SqlServer(string connectionString)
        {
            var options = new DbContextOptionsBuilder<WriteBookStoreContext>()
                .UseSqlServer(connectionString)
                .Options;

            var writeBookStoreContext = new WriteBookStoreContext(options);
            return writeBookStoreContext;
        }
    }
}
