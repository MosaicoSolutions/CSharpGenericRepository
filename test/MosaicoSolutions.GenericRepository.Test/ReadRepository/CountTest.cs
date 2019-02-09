using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class CountTest : ReadRepositoryUnitTest
    {
        public CountTest() : base("BookStoreCountTest")
        {}

        [Fact]
        public async Task CountAsync()
        {
            var countAuthors = await bookStoreContext.Authors.CountAsync();
            (await authorReadRepository.CountAsync()).Should().Be(countAuthors);

            var countBooks = await bookStoreContext.Books.CountAsync();
            (await bookReadRepository.CountAsync()).Should().Be(countBooks);

            var longCountAuthors = await bookStoreContext.Authors.LongCountAsync();
            (await authorReadRepository.LongCountAsync()).Should().Be(longCountAuthors);

            var longCountBooks = await bookStoreContext.Books.LongCountAsync();
            (await bookReadRepository.LongCountAsync()).Should().Be(longCountBooks);

            var booksWithNameNaruto = await bookStoreContext.Books.CountAsync(b => b.Title == "Naruto");
            (await bookReadRepository.CountAsync(b => b.Title == "Naruto")).Should().Be(booksWithNameNaruto);

            var booksContainingDragonInTitle = await bookStoreContext.Books.LongCountAsync(b => b.Title.ToLower().Contains("dragon"));
            (await bookReadRepository.LongCountAsync(b => b.Title.ToLower().Contains("dragon"))).Should().Be(booksContainingDragonInTitle);
        }
    }
}