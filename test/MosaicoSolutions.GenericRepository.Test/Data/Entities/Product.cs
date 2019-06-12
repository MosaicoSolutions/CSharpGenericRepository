using System;
using MosaicoSolutions.GenericRepository.Annotations;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public ProductCategory ProductCategory { get; set; }
        [CreatedAt] public DateTime CreatedAt { get; set; }
        [LastUpdatedAt] public DateTime? LastUpdatedAt { get; set; }
        [RowVersion] public int RowVersion { get; set; }
    }
}
