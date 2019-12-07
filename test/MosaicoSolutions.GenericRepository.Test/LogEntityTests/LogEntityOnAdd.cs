using FluentAssertions;
using Xunit;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;
using MosaicoSolutions.GenericRepository.Data.Entities;
using System;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityOnAdd : LogEntityUnitTest
    {
        [Fact]
        public void ShouldNotLogWhenAddindProducts()
        {
            var random = new Random();
            var newProducts = fakerProduct.Generate(random.Next(1, 3));

            var insertNewProductTask = productTransactionalRepository.InsertRangeAsTransactionTask(newProducts);

            var transactionTaskResult = transactionTaskManager.UseTransaction(insertNewProductTask);
            transactionTaskResult.Success.Should().BeTrue();

            var transactionId = transactionTaskResult.TransactionId.ToString();

            newProducts.ForEach(p =>
            {
                var productIdExpressison = $"\"{nameof(p.ProductId)}\":{p.ProductId}";

                logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) && 
                                                        log.LogActionType == LogActionType.Insert &&
                                                        log.TransactionId == transactionId).Result.Should().BeFalse();
            });
        }

        [Fact]
        public void ShouldLogWhenAddindOrder()
        {
            var newOrder = fakerOrder.Generate();

            var insertNewOrderTask = orderTransactionalRepository.InsertAsTransactionTask(newOrder);

            var transactionTaskResult = transactionTaskManager.UseTransaction(insertNewOrderTask);

            transactionTaskResult.Success.Should().BeTrue();

            var orderIdExpressison = $"\"{nameof(newOrder.OrderId)}\":{newOrder.OrderId}";
            var transactionId = transactionTaskResult.TransactionId.ToString();

            logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(orderIdExpressison) &&
                                                    log.LogActionType == LogActionType.Insert &&
                                                    log.TransactionId == transactionId).Result.Should().BeTrue();
        }
    }
}
