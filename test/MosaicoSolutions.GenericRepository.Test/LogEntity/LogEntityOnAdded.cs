using FluentAssertions;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.LogEntity
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
        }
    }
}
