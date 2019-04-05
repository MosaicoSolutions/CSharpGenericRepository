using System.Linq;
using System.Threading.Tasks;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class InsertTest : WriteRepositoryUnitTest
    {
        private Faker<Author> fakerAuthor;
        private Faker<Book> fakerBook;

        public InsertTest() : base("BookStoreInsert")
        { 
            fakerAuthor = new Faker<Author>()
                            .RuleFor(a => a.FirstName, f => f.Name.FirstName())
                            .RuleFor(a => a.LastName, f => f.Name.LastName());

            fakerBook = new Faker<Book>()
                        .RuleFor(b => b.Title, f => f.Lorem.Word());
        }

        [Fact]
        public void Insert()
        {
            var newAuthor = fakerAuthor.Generate(1).FirstOrDefault();

            authorWriteRepository.Insert(newAuthor);

            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task InsertAsync()
        {
            var newAuthor = fakerAuthor.Generate(1).FirstOrDefault();

            await authorWriteRepository.InsertAsync(newAuthor);

            var rowsAffected = await unitOfWork.CommitAsync();
            rowsAffected.Should().BeGreaterThan(0);
        }

        [Fact]
        public void InsertRange()
        {
            var firstAuthor = bookStoreContext.Author.FirstOrDefault();

            var books = fakerBook.Generate(3);

            books.ForEach(b => b.AuthorId = firstAuthor.AuthorId);

            bookWriteRepository.InsertRange(books);

            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);
        }

        [Fact]
        public async Task InsertRangeAsync()
        {
            var firstAuthor = await bookStoreContext.Author.FirstOrDefaultAsync();

            var books = fakerBook.Generate(3);

            books.ForEach(b => b.AuthorId = firstAuthor.AuthorId);

            await bookWriteRepository.InsertRangeAsync(books);

            var rowsAffected = await unitOfWork.CommitAsync();
            rowsAffected.Should().BeGreaterThan(0);
        }
    }
}