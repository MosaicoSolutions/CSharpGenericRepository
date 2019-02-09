using System.Linq;
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
    }
}