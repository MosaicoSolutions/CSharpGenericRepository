using System;
using MosaicoSolutions.GenericRepository.Repositories.Read;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.ReadRepository
{
    public class ReadRepositoryUnitTest : IDisposable
    {
        protected BookStoreContext bookStoreContext;
        protected IReadRepository<BookStoreContext, Book> bookReadRepository;
        protected IReadRepository<BookStoreContext, Author> authorReadRepository;

        public ReadRepositoryUnitTest(string databaseName)
        {
            bookStoreContext = BookStoreContext.NewDatabaseInMemory(databaseName);
            bookReadRepository = new ReadRepository<BookStoreContext, Book>(bookStoreContext);
            authorReadRepository = new ReadRepository<BookStoreContext, Author>(bookStoreContext);
        }

        public void Dispose() => bookStoreContext.Dispose();
    }
}