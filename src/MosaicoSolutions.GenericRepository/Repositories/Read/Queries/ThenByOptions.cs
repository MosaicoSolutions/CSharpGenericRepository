using System;
using System.Linq.Expressions;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    public struct ThenByOptions<TEntity> where TEntity : class
    {
        public SortDirection? Direction { get; set; }
        public Expression<Func<TEntity, object>> ThenBy { get; set; }
    }
}