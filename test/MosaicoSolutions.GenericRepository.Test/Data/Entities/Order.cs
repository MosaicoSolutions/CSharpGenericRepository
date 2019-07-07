using System.Collections.Generic;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
