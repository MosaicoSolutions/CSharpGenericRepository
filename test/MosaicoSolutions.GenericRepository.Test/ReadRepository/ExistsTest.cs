using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class ExistsTest : ReadRepositoryUnitTest
    {
        public ExistsTest() : base("BookStoreExists")
        {}

        [Fact]
        public void Exists()
        {
            var authors = bookStoreContext.Authors.ToList();
            
            authors.ForEach(a => authorReadRepository.Exists(a.AuthorId).Should().BeTrue());
        }

        [Fact]
        public void ExistsAsync()
        {
            var books = bookStoreContext.Books.ToList();
            
            books.ForEach(async b => (await authorReadRepository.ExistsAsync(b.AuthorId)).Should().BeTrue());
        }

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
    }
}