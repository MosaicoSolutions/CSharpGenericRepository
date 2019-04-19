using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Bogus;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using MosaicoSolutions.GenericRepository.Test.Data.EntityConfigurations;

namespace MosaicoSolutions.GenericRepository.Test.Data.Contexts
{
    public class BookStoreContext : DbContext
    {
        public virtual DbSet<Book> Book { get; set; }
        public virtual DbSet<Author> Author { get; set; }

        public BookStoreContext()
        {}

        public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options) 
        {}

        public static BookStoreContext NewDatabaseInMemory(string databaseName)
        {
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseInMemoryDatabase(databaseName: databaseName)
                .UseLazyLoadingProxies(useLazyLoadingProxies: false)
                .Options;
            
            var bookStoreContext = new BookStoreContext(options);
            bookStoreContext.InitializeDataBase();
            return bookStoreContext;
        }

        public static BookStoreContext SqlServerExpress()
            => SqlServer(@"Server=.\SQLEXPRESS;Database=BookStore;Trusted_Connection=True;");

        public static BookStoreContext SqlServer(string connectionString)
        {
            var options = new DbContextOptionsBuilder<BookStoreContext>()
                .UseSqlServer(connectionString)
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .Options;

            var bookStoreContext = new BookStoreContext(options);
            return bookStoreContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        private void InitializeDataBase()
        {
            CleanDatabase();

            var authors = new List<Author>()
            {
                new Author
                {
                    FirstName = "William",
                    LastName = "Shakespeare",
                    Books = new List<Book>
                    {
                        new Book { Title = "Hamlet"},
                        new Book { Title = "Othello" },
                        new Book { Title = "MacBeth" }
                    }
                },
                new Author
                {
                    FirstName = "Masashi",
                    LastName = "Kishimoto",
                    Books = new List<Book>
                    {
                        new Book { Title = "Naruto" }
                    }
                },
                new Author
                {
                    FirstName = "Akira",
                    LastName = "Toriyama",
                    Books = new List<Book>
                    {
                        new Book { Title = "Dragon Ball" }
                    }
                },
                new Author
                {
                    FirstName = "Machado",
                    LastName = "Assis",
                    Books = new List<Book>
                    {
                        new Book { Title = "Dom Casmurro" },
                        new Book { Title = "Quincas Borba" },
                        new Book { Title = "A mão e a luva" }
                    }
                }
            };

            Author.AddRange(authors);
            SaveChanges();
        }

        private void CleanDatabase()
        {
            Book.RemoveRange(Book.ToList());
            Author.RemoveRange(Author.ToList());
            SaveChanges();
        }

        public void AddFakeData(int? count = null)
        {
            var random = new System.Random();

            var fakeBookGenerator = new Faker<Book>()
                        .RuleFor(b => b.Title, f => f.Lorem.Word());

            var fakeAuthors = new Faker<Author>()
                        .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                        .RuleFor(a => a.LastName, f => f.Name.LastName())
                        .RuleFor(a => a.Books, f => fakeBookGenerator.Generate(random.Next(2, 10)));

            Author.AddRange(fakeAuthors.Generate(count ?? random.Next(20, 50)));
            SaveChanges();
        }
    }
}