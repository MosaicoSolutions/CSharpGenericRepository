using System.Threading.Tasks;
using Xunit;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;
using FluentAssertions;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class QueryableExtensionsTest : ReadRepositoryUnitTest
    {
        public QueryableExtensionsTest() : base("BookStoreQueryableExtensions")
        {}

        [Fact]
        public async Task AnyAsync()
        {
            (await authorReadRepository.AnyAsync()).Should().BeTrue();
            (await bookReadRepository.AnyAsync()).Should().BeTrue();
            
            (await authorReadRepository.AnyAsync(a => a.FirstName == "William")).Should().BeTrue();
            (await bookReadRepository.AnyAsync(b => b.Title == "Helena")).Should().BeFalse();
            (await bookReadRepository.AnyAsync(b => b.Author.LastName == "Assis")).Should().BeTrue();
            (await authorReadRepository.AnyAsync(a => a.Books.Count() >= 3)).Should().BeTrue();
        }

        [Fact]
        public async Task AllAsync()
        {
            (await authorReadRepository.AllAsync(a => a.Books.Any())).Should().BeTrue();
        }

        [Fact]
        public async Task CountAsync()
        {
            var booksCount = await bookStoreContext.Book.CountAsync();
            var booksStartWithA = await bookStoreContext.Book.CountAsync(b => b.Title.ToLower().StartsWith("a"));

            (await bookReadRepository.CountAsync()).Should().Be(booksCount);
            (await bookReadRepository.CountAsync(b => b.Title.ToLower().StartsWith("a"))).Should().Be(booksStartWithA);

            var booksLongCount = await bookStoreContext.Book.LongCountAsync();
            var booksLongStartWithA = await bookStoreContext.Book.LongCountAsync(b => b.Title.ToLower().StartsWith("a"));

            (await bookReadRepository.LongCountAsync()).Should().Be(booksCount);
            (await bookReadRepository.LongCountAsync(b => b.Title.ToLower().StartsWith("a"))).Should().Be(booksStartWithA);
        }

        [Fact]
        public async Task MinAsync()
        {
            var minBookCount = await bookStoreContext.Author.MinAsync(a => a.Books.Count());
            var minBookCountFromRepository = await authorReadRepository.MinAsync(a => a.Books.Count());

            minBookCountFromRepository.Should().Be(minBookCount);
        }

        [Fact]
        public async Task MaxAsync()
        {
            var maxBookCount = await bookStoreContext.Author.MaxAsync(a => a.Books.Count());
            var maxBookCountFromRepository = await authorReadRepository.MaxAsync(a => a.Books.Count());

            maxBookCountFromRepository.Should().Be(maxBookCount);
        }

        [Fact]
        public async Task SumAsync()
        {
            var sumBook = await bookStoreContext.Author.SumAsync(a => a.Books.Count());
            var sumBookFromRepository = await authorReadRepository.SumAsync(a => a.Books.Count());

            sumBookFromRepository.Should().Be(sumBook);
        }

        [Fact]
        public async Task AverageAsync()
        {
            var averageBook = await bookStoreContext.Author.AverageAsync(a => a.Books.Count());
            var averangeBookFromRepository = await authorReadRepository.AverageAsync(a => a.Books.Count());

            averangeBookFromRepository.Should().Be(averageBook);
        }

        [Fact]
        public async Task ContainsAsync()
        {
            var firstAuthor = await bookStoreContext.Author.FirstOrDefaultAsync();

            (await authorReadRepository.ContainsAsync(firstAuthor)).Should().BeTrue();
        }
    }
}