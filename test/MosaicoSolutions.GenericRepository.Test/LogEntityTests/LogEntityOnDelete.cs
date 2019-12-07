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
        public void DeleteOrdemItem()
        {
            var firstOrderItem = marketplaceContext.Set<OrderItem>()
                                                   .FirstOrDefault();

            if (firstOrderItem is null) return;

            var deleteTransactionTask = orderItemTransactionalRepository.RemoveAsTransactionTask(firstOrderItem);

            var transactionTaskResult = transactionTaskManager.UseTransaction(deleteTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();

            var orderItemIdExpressison = $"\"{nameof(firstOrderItem.OrdemItemId)}\":{firstOrderItem.OrdemItemId}";
            var transactionId = transactionTaskResult.TransactionId.ToString();

            logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(orderItemIdExpressison) &&
                                                    log.LogActionType == LogActionType.Delete &&
                                                    log.TransactionId == transactionId).Result.Should().BeTrue();

            marketplaceContext.Set<OrderItem>().Any(o => o.OrdemItemId == firstOrderItem.OrdemItemId).Should().BeFalse();
        }

        [Fact]
        public void DeleteOrdemItems()
        {
            var random = new Random();
            var countOrderItems = random.Next(5);
            var orderItemsToDelete = marketplaceContext.Set<OrderItem>()
                                                       .Take(countOrderItems)
                                                       .ToList();

            var deleteTransactionTask = orderItemTransactionalRepository.RemoveRangeAsTransactionTask(orderItemsToDelete);

            var transactionTaskResult = transactionTaskManager.UseTransaction(deleteTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();
            var transactionId = transactionTaskResult.TransactionId.ToString();

            orderItemsToDelete.ForEach(p =>
            {
                var orderItemIdExpressison = $"\"{nameof(p.OrdemItemId)}\":{p.OrdemItemId}";

                logEntityReadRepository.AnyAsync(log => log.OriginalValues.Contains(orderItemIdExpressison) && 
                                                        log.LogActionType == LogActionType.Delete &&
                                                        log.TransactionId == transactionId).Result.Should().BeTrue();
            });
        }
    }
}
