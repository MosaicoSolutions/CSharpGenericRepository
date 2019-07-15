using MosaicoSolutions.GenericRepository.Annotations;
using System;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class OrderItem
    {
        public int OrdemItemId { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public int Amount { get; set; }
        [CreatedAt] public DateTime CreatedAt { get; set; }
        public Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}