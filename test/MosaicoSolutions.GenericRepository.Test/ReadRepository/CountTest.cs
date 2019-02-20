using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class CountTest : ReadRepositoryUnitTest
    {
        public CountTest() : base("BookStoreCountTest")
        {}

        [Fact]
        public async Task CountAsync()
        {
            var countAuthors = await bookStoreContext.Author.CountAsync();
            (await authorReadRepository.CountAsync()).Should().Be(countAuthors);

            var countBooks = await bookStoreContext.Book.CountAsync();
            (await bookReadRepository.CountAsync()).Should().Be(countBooks);

            var longCountAuthors = await bookStoreContext.Author.LongCountAsync();
            (await authorReadRepository.LongCountAsync()).Should().Be(longCountAuthors);

            var longCountBooks = await bookStoreContext.Book.LongCountAsync();
            (await bookReadRepository.LongCountAsync()).Should().Be(longCountBooks);

            var booksWithNameNaruto = await bookStoreContext.Book.CountAsync(b => b.Title == "Naruto");
            (await bookReadRepository.CountAsync(b => b.Title == "Naruto")).Should().Be(booksWithNameNaruto);

            var booksContainingDragonInTitle = await bookStoreContext.Book.LongCountAsync(b => b.Title.ToLower().Contains("dragon"));
            (await bookReadRepository.LongCountAsync(b => b.Title.ToLower().Contains("dragon"))).Should().Be(booksContainingDragonInTitle);
        }
    }
}