using System.Linq;
using FluentAssertions;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class RemoveTest : WriteRepositoryUnitTest
    {
        public RemoveTest() : base("BookStoreRemove")
        { }

        [Fact]
        public void RemoveById()
        {
            var firstAuthor = bookStoreContext.Author.FirstOrDefault();
            
            bookWriteRepository.RemoveById(firstAuthor.AuthorId);
            
            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            var authorAgain = bookStoreContext.Author.FirstOrDefault(a => a.AuthorId == firstAuthor.AuthorId);

            bookStoreContext.Author.Any(a => a.AuthorId == firstAuthor.AuthorId).Should().BeFalse();
        }

        [Fact]
        public void RemoveWhere()
        {
            authorWriteRepository.RemoveWhere(a => a.Books.Any(b => b.Title == "Naruto"));

            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            bookStoreContext.Author.Any(a => a.Books.Any(b => b.Title == "Naruto")).Should().BeFalse();
        }
    }
}