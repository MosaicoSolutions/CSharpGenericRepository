using System;
using System.Linq.Expressions;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    public struct QueryOptions<TEntity> where TEntity : class
    {
        public bool Tracking { get; set; }
        public Expression<Func<TEntity, bool>> Where { get; set; }
        public Expression<Func<TEntity, object>>[] Includes { get; set; }
        public SortOptions<TEntity> Sort { get; set; }
    }
}