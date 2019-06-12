using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.LogEntity
{
    public class LogEntityOnAdded : LogEntityUnitTest
    {
        [Fact]
        public void AddProduct()
        {
            var newProduct = fakerProduct.Generate();

            marketplaceWriteRepository.Insert(newProduct);
            unitOfWork.Commit();
        }
    }
}
