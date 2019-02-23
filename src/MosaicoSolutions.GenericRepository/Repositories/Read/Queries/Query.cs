using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    internal class Query<TEntity> : IQuery<TEntity> where TEntity : class
    {
        private IQueryable<TEntity> queryable;

        internal Query(DbSet<TEntity> dbSet, QueryOptions<TEntity> queryOptions)
        {
            queryable = queryOptions.Where is null
                            ? dbSet
                            : dbSet.Where(queryOptions.Where);

            queryable = queryOptions.Tracking
                            ? queryable.AsTracking()
                            : queryable.AsNoTracking();
            
            var sortOptions = queryOptions.Sort;

            if (sortOptions.OrderBy != null)
            {
                var sortDirection = sortOptions.Direction ?? SortDirection.Ascending;

                var orderedQueryable =  sortDirection == SortDirection.Ascending
                                            ? queryable.OrderBy(sortOptions.OrderBy)
                                            : queryable.OrderByDescending(sortOptions.OrderBy);

                if (sortOptions.ThenBy?.Length > 0)
                    foreach (var thenBy in sortOptions.ThenBy)
                        orderedQueryable = orderedQueryable.ThenBy(thenBy);

                queryable = orderedQueryable;
            }

            if (queryOptions.Includes?.Length > 0)
                foreach (var include in queryOptions.Includes)
                    queryable = queryable.Include(include);
        }

        public TEntity First() => queryable.First();

        public Task<TEntity> FirstAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.FirstAsync(cancellationToken);

        public TEntity FirstOrDefault() => queryable.FirstOrDefault();

        public Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.FirstOrDefaultAsync(cancellationToken);

        public TEntity Last() => queryable.Last();

        public Task<TEntity> LastAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.LastAsync(cancellationToken);

        public TEntity LastOrDefault() => queryable.LastOrDefault();

        public Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.LastOrDefaultAsync(cancellationToken);

        public TEntity Single() => queryable.Single();

        public Task<TEntity> SingleAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.SingleAsync(cancellationToken);

        public TEntity SingleOrDefault() => queryable.SingleOrDefault();

        public Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.SingleOrDefaultAsync(cancellationToken);

        public List<TEntity> ToList() => queryable.ToList();

        public Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken)) => queryable.ToListAsync(cancellationToken);
    }
}