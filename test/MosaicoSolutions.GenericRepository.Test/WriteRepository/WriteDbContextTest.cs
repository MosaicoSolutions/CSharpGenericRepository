using System;
using System.Linq;
using Bogus;
using FluentAssertions;
using MosaicoSolutions.GenericRepository.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class WriteDbContextTest
    {
        private readonly WriteDbContext writeDbContext;
        private readonly Faker<Author> fakerAuthor;

        public WriteDbContextTest()
        {
            writeDbContext = WriteBookStoreContext.SqlServerDocker();
            fakerAuthor = new Faker<Author>()
                                .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                                .RuleFor(a => a.LastName, f => f.Name.LastName());
        }

        [Fact]
        public void CreateNew()
        {
            var newAuthor = fakerAuthor.Generate();

            writeDbContext.Set<Author>().Add(newAuthor);
            var rowsAffected = writeDbContext.SaveChanges();

            rowsAffected.Should().BeGreaterThan(0);
            newAuthor.CreatedAt.Should().BeSameDateAs(DateTime.UtcNow);
            newAuthor.RowVersion.Should().BeGreaterThan(0);
        }

        [Fact]
        public void LastUpdatedAt()
        {
            var firstAuthor = writeDbContext.Set<Author>().FirstOrDefault();
            var rowVersion = firstAuthor.RowVersion;

            var firstName = firstAuthor.FirstName;
            firstAuthor.FirstName = firstAuthor.LastName;
            firstAuthor.LastName = firstName;
            
            var rowsAffected = writeDbContext.SaveChanges();
            firstAuthor.LastUpdatedAt.Should().NotBeNull();
            firstAuthor.LastUpdatedAt.Should().BeSameDateAs(DateTime.Now);
            firstAuthor.RowVersion.Should().BeGreaterThan(rowVersion);
        }
    }
}