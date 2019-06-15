using Bogus;
using MosaicoSolutions.GenericRepository.Data.Entities;
using MosaicoSolutions.GenericRepository.Repositories.Read;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using System;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityUnitTest : IDisposable
    {
        protected MarketplaceContext marketplaceContext;
        protected ITransactionalRepository<Product> marketplaceTransactionalRepository;
        protected TransactionTaskManager<MarketplaceContext> transactionTaskManager;
        protected ReadRepository<MarketplaceContext, LogEntity> logEntityReadRepository; 
        protected Faker<Product> fakerProduct;

        public LogEntityUnitTest()
        {
            marketplaceContext = MarketplaceContext.SqlServerExpress();
            marketplaceTransactionalRepository = new TransactionalRepository<Product>();
            transactionTaskManager = new TransactionTaskManager<MarketplaceContext>(MarketplaceContext.SqlServerExpress);
            logEntityReadRepository = new DefaultReadRepository<MarketplaceContext, LogEntity>(marketplaceContext);
            fakerProduct = new Faker<Product>()
                                .RuleFor(p => p.ProductName, f => f.Name.Random.Word())
                                .RuleFor(p => p.ProductCategory, f => f.Random.Enum<ProductCategory>());
        }

        public void Dispose() => marketplaceContext.Dispose();
    }
}
