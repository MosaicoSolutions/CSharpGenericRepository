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
        protected ReadRepository<BookStoreContext, Book> bookReadRepository;
        protected ReadRepository<BookStoreContext, Author> authorReadRepository;

        public ReadRepositoryUnitTest(string databaseName)
        {
            bookStoreContext = BookStoreContext.NewDatabaseInMemory(databaseName);
            bookReadRepository = new DefaultReadRepository<BookStoreContext, Book>(bookStoreContext);
            authorReadRepository = new DefaultReadRepository<BookStoreContext, Author>(bookStoreContext);
        }

        public void Dispose() => bookStoreContext.Dispose();
    }
}