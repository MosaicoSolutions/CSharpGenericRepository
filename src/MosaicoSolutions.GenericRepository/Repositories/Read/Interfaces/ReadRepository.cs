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

        public bool Exists(params object[] ids)
        {
            var entity = DbSet.Find(ids);
            return entity != null;
        }

        public Task<bool> ExistsAsync(object[] ids, CancellationToken cancellationToken = default(CancellationToken))
            => DbSet.FindAsync(ids, cancellationToken).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });

        public Task<bool> ExistsAsync(params object[] ids)
            => DbSet.FindAsync(ids).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });

        public TEntity FindById(params object[] ids)
            => DbSet.Find(ids);    
        
        public Task<TEntity> FindByIdAsync(params object[] ids)
            => DbSet.FindAsync(ids); 

        public Task<TEntity> FindByIdAsync(object[] ids, CancellationToken cancellationToken = default(CancellationToken))
            => DbSet.FindAsync(ids, cancellationToken);

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