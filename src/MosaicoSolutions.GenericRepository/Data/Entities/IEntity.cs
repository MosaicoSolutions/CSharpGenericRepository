using System;
using MosaicoSolutions.GenericRepository.Annotations;

namespace MosaicoSolutions.GenericRepository.Data.Entities
{
    public interface IEntity
    {
        [CreatedAt(UseUtc = true)] DateTime CreatedAt { get; set; }
        [LastUpdatedAt] DateTime? LastUpdatedAt { get; set; }
        [RowVersion] int RowVersion { get; set; }
    }

    public interface IEntity<TKey> : IEntity
    {
        TKey Id { get; set; }
    }
}