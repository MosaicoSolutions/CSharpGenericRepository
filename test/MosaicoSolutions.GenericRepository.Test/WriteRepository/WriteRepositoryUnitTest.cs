using MosaicoSolutions.GenericRepository.Repositories.Write;
using MosaicoSolutions.GenericRepository.Repositories.Write.Interfaces;
using MosaicoSolutions.GenericRepository.Repositories.Write.UoW;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.WriteRepository
{
    public class WriteRepositoryUnitTest
    {
        protected BookStoreContext bookStoreContext;
        protected WriteRepository<BookStoreContext, Book> bookWriteRepository;
        protected WriteRepository<BookStoreContext, Author> authorWriteRepository;
        protected IUnitOfWork<BookStoreContext> unitOfWork; 

        public WriteRepositoryUnitTest(string databaseName)
        {
            bookStoreContext = BookStoreContext.NewDatabaseInMemory(databaseName);
            bookWriteRepository = new DefaultWriteRepository<BookStoreContext, Book>(bookStoreContext);
            authorWriteRepository = new DefaultWriteRepository<BookStoreContext, Author>(bookStoreContext);
            unitOfWork = new UnitOfWork<BookStoreContext>(bookStoreContext);
        }

        public void Dispose() => unitOfWork.Dispose();
    }
}