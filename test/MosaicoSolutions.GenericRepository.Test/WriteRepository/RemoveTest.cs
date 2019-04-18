using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
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
            var firstAuthor = bookStoreContext.Author.AsNoTracking().FirstOrDefault();
            var booksFirstAuthor = bookStoreContext.Book.AsNoTracking().Where(b => b.AuthorId == firstAuthor.AuthorId).ToList();
            
            booksFirstAuthor.ForEach(b => bookWriteRepository.RemoveById(b.BookId));
            
            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            bookStoreContext.Author.Any(a => a.AuthorId == firstAuthor.AuthorId).Should().BeTrue();
            bookStoreContext.Book.Any(b => b.AuthorId == firstAuthor.AuthorId).Should().BeFalse();
        }

        [Fact]
        public void RemoveWhere()
        {
            bookWriteRepository.RemoveWhere(b => b.Title == "Naruto");

            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            bookStoreContext.Book.Any(b => b.Title == "Naruto").Should().BeFalse();
        }
    }
}