using System.Linq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class UpdateTest : WriteRepositoryUnitTest
    {
        public UpdateTest() : base("BookStoreUpdate")
        {}

        [Fact]
        public void Update()
        {
            var bookNaruto = bookStoreContext.Book.FirstOrDefault(b => b.Title.Contains("Naruto"));
            var newTitle = "Naruto Classic";

            bookNaruto.Title = newTitle;

            bookWriteRepository.Update(bookNaruto);
            
            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            bookStoreContext.Book.Any(b => b.Title == newTitle).Should().BeTrue();
        }

        [Fact]
        public void UpdateRange()
        {
            var firstAuthor = bookStoreContext.Author.FirstOrDefault();
            var lastAuthor = bookStoreContext.Author.LastOrDefault();
            var booksLastAuthor = bookStoreContext.Book.Where(b => b.AuthorId == lastAuthor.AuthorId).ToList();

            booksLastAuthor.ForEach(b => b.AuthorId = firstAuthor.AuthorId);

            bookWriteRepository.UpdateRange(booksLastAuthor);

            var rowsAffected = unitOfWork.Commit();
            rowsAffected.Should().BeGreaterThan(0);

            bookStoreContext.Author.Any(a => a.AuthorId == lastAuthor.AuthorId && a.Books.Any()).Should().BeFalse();
        }
    }
}