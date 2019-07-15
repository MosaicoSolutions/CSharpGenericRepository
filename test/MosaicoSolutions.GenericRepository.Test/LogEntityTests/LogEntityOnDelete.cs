using System.Linq;
using FluentAssertions;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;
using Xunit;
using MosaicoSolutions.GenericRepository.Data.Entities;
using System;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityOnDelete : LogEntityUnitTest
    {
        [Fact]
        public void DeleteProduct()
        {
            var firstProduct = marketplaceContext.Set<Product>().FirstOrDefault();

            var deleteTransactionTask = productTransactionalRepository.RemoveAsTransactionTask(firstProduct);

            var transactionTaskResult = transactionTaskManager.UseTransaction(deleteTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();

            var productIdExpressison = $"\"{nameof(firstProduct.ProductId)}\":{firstProduct.ProductId}";
            var transactionId = transactionTaskResult.TransactionId.ToString();

            logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) &&
                                                    log.LogActionType == LogActionType.Delete &&
                                                    log.TransactionId == transactionId).Result.Should().BeTrue();

            marketplaceContext.Set<Product>().Any(p => p.ProductId == firstProduct.ProductId).Should().BeFalse();
        }

        [Fact]
        public void DeleteProducts()
        {
            var random = new Random();
            var countProducts = random.Next(5);
            var productsToDelete = marketplaceContext.Set<Product>()
                                                     .Take(countProducts)
                                                     .ToList();

            var deleteTransactionTask = productTransactionalRepository.RemoveRangeAsTransactionTask(productsToDelete);

            var transactionTaskResult = transactionTaskManager.UseTransaction(deleteTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();
            var transactionId = transactionTaskResult.TransactionId.ToString();

            productsToDelete.ForEach(p =>
            {
                var productIdExpressison = $"\"{nameof(p.ProductId)}\":{p.ProductId}";

                logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) && 
                                                        log.LogActionType == LogActionType.Delete &&
                                                        log.TransactionId == transactionId).Result.Should().BeTrue();
            });
        }
    }
}
