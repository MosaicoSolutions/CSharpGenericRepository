using FluentAssertions;
using Xunit;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;
using System;
using MosaicoSolutions.GenericRepository.Data.Entities;
using System.Threading.Tasks;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityOnAdded : LogEntityUnitTest
    {
        [Fact]
        public void AddProduct()
        {
            var newProduct = fakerProduct.Generate();

            var insertNewProductTask = marketplaceTransactionRepository.InsertAsTransactionTask(newProduct);

            var transactionTaskResult = transactionTaskManager.UseTransaction(insertNewProductTask);

            transactionTaskResult.Success.Should().BeTrue();

            var productIdExpressison = $"\"{nameof(newProduct.ProductId)}\":{newProduct.ProductId}";
            var transactionId = transactionTaskResult.TransactionId.ToString();

            logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) &&
                                                    log.LogActionType == LogActionType.Insert &&
                                                    log.TransactionId == transactionId).Result.Should().BeTrue();
        }

        [Fact]
        public void AddProducts()
        {
            var random = new Random();
            var newProducts = fakerProduct.Generate(random.Next(10));

            var insertNewProductTask = marketplaceTransactionRepository.InsertRangeAsTransactionTask(newProducts);

            var transactionTaskResult = transactionTaskManager.UseTransaction(insertNewProductTask);

            transactionTaskResult.Success.Should().BeTrue();
            var transactionId = transactionTaskResult.TransactionId.ToString();

            newProducts.ForEach(p =>
            {
                var productIdExpressison = $"\"{nameof(p.ProductId)}\":{p.ProductId}";

                logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) && 
                                                        log.LogActionType == LogActionType.Insert &&
                                                        log.TransactionId == transactionId).Result.Should().BeTrue();
            });
        }
    }
}
