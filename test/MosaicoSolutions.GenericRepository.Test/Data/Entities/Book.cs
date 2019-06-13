using MosaicoSolutions.GenericRepository.Annotations;
using MosaicoSolutions.GenericRepository.Data.Entities;
using System;

namespace MosaicoSolutions.GenericRepository.Test.Data.Entities
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        [CreatedAt(UseUtc = true)] public DateTime CreatedAt { get; set; }
        [LastUpdatedAt] public DateTime? LastUpdatedAt { get; set; }
        [RowVersion] public int RowVersion { get; set; }
        public virtual Author Author { get; set; }
    }
}