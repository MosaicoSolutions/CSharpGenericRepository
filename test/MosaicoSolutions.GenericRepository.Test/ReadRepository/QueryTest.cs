using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Queries;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class QueryTest : ReadRepositoryUnitTest
    {
        public QueryTest() : base("BookStoreFind")
        {}

        [Fact]
        public void Find()
        { 
            var firstBookId = bookStoreContext.Book.FirstOrDefault()?.BookId;

            var firstBook = bookReadRepository.Find(firstBookId);

            firstBook.Should().NotBeNull();
            firstBook.BookId.Should().Be(firstBookId);
        }

        [Fact]
        public async Task FindAsync()
        { 
            var firstBookId = (await bookStoreContext.Book.FirstOrDefaultAsync())?.BookId;

            var firstBook = await bookReadRepository.FindAsync(firstBookId);

            firstBook.Should().NotBeNull();
            firstBook.BookId.Should().Be(firstBookId);
        }

        [Fact]
        public void FindWithQueryOptions()
        {
            var firstAuthorId = bookStoreContext.Author.FirstOrDefault(a => a.Books.Count() == 3)?.AuthorId;

            var queryOptions = new QueryOptions<Author>
            {
                Where = a => a.Books.Count() == 3
            };

            var firstAuthorWithThreeBooks = authorReadRepository.Find(queryOptions).FirstOrDefault();

            firstAuthorWithThreeBooks.Should().NotBeNull();
            firstAuthorWithThreeBooks.AuthorId.Should().Be(firstAuthorId);
        }

        [Fact]
        public async Task FindWithQueryOptionsAsync()
        {
            var firstAuthorId = (await bookStoreContext.Author.FirstOrDefaultAsync(a => a.Books.Count() == 3))?.AuthorId;

            var queryOptions = new QueryOptions<Author>
            {
                Where = a => a.Books.Count() == 3
            };

            var firstAuthorWithThreeBooks = (await authorReadRepository.FindAsync(queryOptions)).FirstOrDefault();

            firstAuthorWithThreeBooks.Should().NotBeNull();
            firstAuthorWithThreeBooks.AuthorId.Should().Be(firstAuthorId);
        }

        [Fact]
        public void UsingSort()
        {
            var authors = bookStoreContext.Author.AsNoTracking().OrderBy(a => a.FirstName).ToList();
            
            var queryOptions = new QueryOptions<Author>
            {
                Sort = new SortOptions<Author>
                {
                    OrderBy = a => a.FirstName
                }
            };

            var orderedAuthors = authorReadRepository.Find(queryOptions);

            orderedAuthors.SequenceEqual(authors, new AuthorEqualityComparer()).Should().BeTrue();
        }

        [Fact]
        public async Task UsingSortAsync()
        {
            var authors = await bookStoreContext.Author.OrderBy(a => a.FirstName).ToListAsync();
            
            var queryOptions = new QueryOptions<Author>
            {
                Sort = new SortOptions<Author>
                {
                    OrderBy = a => a.FirstName
                }
            };

            var orderedAuthors = await authorReadRepository.FindAsync(queryOptions);

            orderedAuthors.SequenceEqual(authors, new AuthorEqualityComparer()).Should().BeTrue();
        }

        class AuthorEqualityComparer : IEqualityComparer<Author>
        {
            public bool Equals(Author x, Author y)
                => x.AuthorId == y.AuthorId;

            public int GetHashCode(Author obj)
                => obj.AuthorId.GetHashCode();
        }
    }
}