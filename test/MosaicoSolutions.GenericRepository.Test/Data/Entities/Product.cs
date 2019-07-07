﻿using System;
using System.Collections.Generic;
using MosaicoSolutions.GenericRepository.Annotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        [JsonConverter(typeof(StringEnumConverter))] public ProductCategory ProductCategory { get; set; }
        [CreatedAt] public DateTime CreatedAt { get; set; }
        [LastUpdatedAt] public DateTime? LastUpdatedAt { get; set; }
        [RowVersion] public int RowVersion { get; set; }

        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
