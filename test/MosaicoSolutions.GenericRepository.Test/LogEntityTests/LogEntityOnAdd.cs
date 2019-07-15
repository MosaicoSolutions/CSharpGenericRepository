using FluentAssertions;
using Xunit;
using MosaicoSolutions.GenericRepository.Repositories.Read.Extensions;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityOnAdd : LogEntityUnitTest
    {
        [Fact]
        public void ShouldNotLogWhenAddindProduct()
        {
            var newProduct = fakerProduct.Generate();

            var insertNewProductTask = productTransactionalRepository.InsertAsTransactionTask(newProduct);

            var transactionTaskResult = transactionTaskManager.UseTransaction(insertNewProductTask);

            transactionTaskResult.Success.Should().BeTrue();

            var productIdExpressison = $"\"{nameof(newProduct.ProductId)}\":{newProduct.ProductId}";
            var transactionId = transactionTaskResult.TransactionId.ToString();

            logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(productIdExpressison) &&
                                                    log.LogActionType == LogActionType.Insert &&
                                                    log.TransactionId == transactionId).Result.Should().BeFalse();
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
