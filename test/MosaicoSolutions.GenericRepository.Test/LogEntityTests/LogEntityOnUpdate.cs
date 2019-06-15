using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Test.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityOnUpdate : LogEntityUnitTest
    {
        [Fact]
        public void UpdateProduct()
        {
            var countProduct = marketplaceContext.Set<Product>().Count();
            var random = new Random();
            var skipProducts = random.Next(countProduct - 1);
            var productToUpdate = marketplaceContext.Set<Product>()
                                                    .AsNoTracking()
                                                    .OrderBy(p => p.ProductId)
                                                    .Skip(skipProducts)
                                                    .FirstOrDefault();

            if (productToUpdate is null)
                return;

            var newProduct = fakerProduct.Generate();

            productToUpdate.ProductCategory = newProduct.ProductCategory;
            productToUpdate.ProductName = newProduct.ProductName;
            productToUpdate.Price = random.Next(999);

            var updateProductTransactionTask = marketplaceTransactionalRepository.UpdateAsTransactionTask(productToUpdate);

            var transactionTaskResult = transactionTaskManager.UseTransaction(updateProductTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();
        }

        [Fact]
        public void UpdateProducts()
        {
            var countProduct = marketplaceContext.Set<Product>().Count();
            var random = new Random();
            var skipProducts = random.Next(countProduct - 1);
            var maxTake = countProduct - skipProducts == 1 ? 1 : skipProducts;
            var take = random.Next(maxTake);
            var productsToUpdate = marketplaceContext.Set<Product>()
                                                    .AsNoTracking()
                                                    .OrderBy(p => p.ProductId)
                                                    .Skip(skipProducts)
                                                    .Take(take)
                                                    .ToList();

            if (!productsToUpdate.Any())
                return;

            var newProducts = fakerProduct.Generate(productsToUpdate.Count);

            for (var i = 0; i < productsToUpdate.Count; i++)
            {
                productsToUpdate[i].ProductCategory = newProducts[i].ProductCategory;
                productsToUpdate[i].ProductName = newProducts[i].ProductName;
                productsToUpdate[i].Price = random.Next(999);
            }

            var updateProductTransactionTask = marketplaceTransactionalRepository.UpdateRangeAsTransactionTask(productsToUpdate);

            var transactionTaskResult = transactionTaskManager.UseTransaction(updateProductTransactionTask);

            transactionTaskResult.Success.Should().BeTrue();
        }
    }
}
