using MosaicoSolutions.GenericRepository.Annotations;
using MosaicoSolutions.GenericRepository.Data.Entities;
using System;
using System.Collections.Generic;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class Author : IEntity
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        [CreatedAt(UseUtc = true)] public DateTime CreatedAt { get; set; }
        [LastUpdatedAt] public DateTime? LastUpdatedAt { get; set; }
        [RowVersion] public int RowVersion { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}