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
            var authors = bookStoreContext.Author.ToList();
            
            authors.ForEach(a => authorReadRepository.Exists(a.AuthorId).Should().BeTrue());
        }

        [Fact]
        public void ExistsAsync()
        {
            var books = bookStoreContext.Book.ToList();
            
            books.ForEach(async b => (await authorReadRepository.ExistsAsync(b.AuthorId)).Should().BeTrue());
        }
    }
}