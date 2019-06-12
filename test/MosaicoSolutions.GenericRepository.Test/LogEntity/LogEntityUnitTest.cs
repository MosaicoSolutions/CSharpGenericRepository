using Bogus;
using MosaicoSolutions.GenericRepository.Repositories.Write;
using MosaicoSolutions.GenericRepository.Repositories.Write.Interfaces;
using MosaicoSolutions.GenericRepository.Repositories.Write.UoW;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using System;

namespace MosaicoSolutions.GenericRepository.Test.LogEntity
{
    public class LogEntityUnitTest : IDisposable
    {
        protected MarketplaceContext marketplaceContext;
        protected WriteRepository<MarketplaceContext, Product> marketplaceWriteRepository;
        protected IUnitOfWork<MarketplaceContext> unitOfWork;
        protected Faker<Product> fakerProduct;

        public LogEntityUnitTest()
        {
            marketplaceContext = MarketplaceContext.SqlServerExpress();
            marketplaceWriteRepository = new DefaultWriteRepository<MarketplaceContext, Product>(marketplaceContext);
            unitOfWork = new UnitOfWork<MarketplaceContext>(marketplaceContext);
            fakerProduct = new Faker<Product>()
                                .RuleFor(p => p.ProductName, f => f.Name.Random.Word())
                                .RuleFor(p => p.ProductCategory, f => f.Random.Enum<ProductCategory>());
        }

        public void Dispose() => marketplaceContext.Dispose();
    }
}
