using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Queries;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces
{
    public abstract class ReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                              where TEntity: class
    {
        public TDbContext DbContext { get; }
        public DbSet<TEntity> DbSet { get; }

        public ReadRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public bool Exists(params object[] keyValues)
        {
            var entity = DbSet.Find(keyValues);
            return entity != null;
        }

        public Task<bool> ExistsAsync(object[] keyValues, CancellationToken cancellationToken = default(CancellationToken))
            => DbSet.FindAsync(keyValues, cancellationToken).ContinueWith(task => task.Result != null);

        public Task<bool> ExistsAsync(params object[] keyValues)
            => DbSet.FindAsync(keyValues).ContinueWith(task => task.Result != null);

        public TEntity FindById(params object[] keyValues)
            => DbSet.Find(keyValues);    
        
        public Task<TEntity> FindByIdAsync(params object[] keyValues)
            => DbSet.FindAsync(keyValues); 

        public Task<TEntity> FindByIdAsync(object[] keyValues, CancellationToken cancellationToken = default(CancellationToken))
            => DbSet.FindAsync(keyValues, cancellationToken);

        public List<TEntity> Find(QueryOptions<TEntity> queryOptions)
        {
            var query = Query(queryOptions);
            return query.ToList();
        }

        public Task<List<TEntity>> FindAsync(QueryOptions<TEntity> queryOptions)
        {
            var query = Query(queryOptions);
            return query.ToListAsync();
        }

        public IQuery<TEntity> Query(QueryOptions<TEntity> queryOptions) => new Query<TEntity>(DbSet, queryOptions);
    }
}