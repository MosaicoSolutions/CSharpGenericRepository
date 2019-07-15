using System;
using System.Collections.Generic;
using MosaicoSolutions.GenericRepository.Annotations;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    [EntityLog(LogActionType = LogActionType.Insert | LogActionType.Delete)]
    public class Order
    {
        public int OrderId { get; set; }
        public string Customer { get; set; }
        [CreatedAt] public DateTime CreatedAt { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
