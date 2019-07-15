using Bogus;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Data.Entities;
using MosaicoSolutions.GenericRepository.Repositories.Read;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using System;
using System.Linq;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityUnitTest : IDisposable
    {
        protected MarketplaceContext marketplaceContext;
        protected ITransactionalRepository<Product> productTransactionalRepository;
        protected ITransactionalRepository<Order> orderTransactionalRepository;
        protected TransactionTaskManager<MarketplaceContext> transactionTaskManager;
        protected ReadRepository<MarketplaceContext, LogEntity> logEntityReadRepository; 
        protected Faker<Product> fakerProduct;
        protected Faker<Order> fakerOrder;
        protected Faker<OrderItem> fakerOrderItem;

        public LogEntityUnitTest()
        {
            marketplaceContext = MarketplaceContext.SqlServerExpress();
            productTransactionalRepository = new TransactionalRepository<Product>();
            orderTransactionalRepository = new TransactionalRepository<Order>();
            transactionTaskManager = new TransactionTaskManager<MarketplaceContext>(MarketplaceContext.SqlServerExpress);
            logEntityReadRepository = new DefaultReadRepository<MarketplaceContext, LogEntity>(marketplaceContext);

            var products = marketplaceContext.Set<Product>().AsNoTracking().ToList();

            fakerProduct = new Faker<Product>()
                                .RuleFor(p => p.ProductName, f => f.Name.Random.Word())
                                .RuleFor(p => p.ProductCategory, f => f.Random.Enum<ProductCategory>());

            fakerOrderItem = new Faker<OrderItem>()
                                    .RuleFor(o => o.Amount, f => f.Random.Int(min: 1, max: 10))
                                    .RuleFor(o => o.ProductId, f => f.PickRandom(products.Select(p => p.ProductId).ToList()));

            fakerOrder = new Faker<Order>()
                                .RuleFor(p => p.Customer, f => f.Name.FirstName())
                                .RuleFor(o => o.OrderItem, _ => fakerOrderItem.Generate(2));
        }

        public void Dispose() => marketplaceContext.Dispose();
    }
}
