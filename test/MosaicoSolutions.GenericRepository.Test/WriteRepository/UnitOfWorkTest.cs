using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class UnitOfWorkTest : WriteRepositoryUnitTest
    {
        public UnitOfWorkTest() : base("UnitOfWorkTest")
        {
        }

        [Fact]
        public void RejectChanges()
        {
            var firstAuthor = bookStoreContext.Author.FirstOrDefault();
            var booksFirstAuthor = bookStoreContext.Book.Where(b => b.AuthorId == firstAuthor.AuthorId).ToList();

            firstAuthor.FirstName = "New Name";
            authorWriteRepository.Update(firstAuthor);
            bookWriteRepository.RemoveRange(booksFirstAuthor);

            bookStoreContext.ChangeTracker.Entries<Book>().Any(entry => entry.State != EntityState.Unchanged).Should().BeTrue();
            bookStoreContext.ChangeTracker.Entries<Author>().Any(entry => entry.State != EntityState.Unchanged).Should().BeTrue();

            unitOfWork.RejectChanges();

            bookStoreContext.ChangeTracker.Entries<Book>().Any(entry => entry.State != EntityState.Unchanged).Should().BeFalse();
            bookStoreContext.ChangeTracker.Entries<Author>().Any(entry => entry.State != EntityState.Unchanged).Should().BeFalse();

            unitOfWork.Commit().Should().Be(0);
        }
    }
}