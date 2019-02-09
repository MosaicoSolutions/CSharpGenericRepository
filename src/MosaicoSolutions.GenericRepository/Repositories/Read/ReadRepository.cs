using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Read
{
    public class ReadRepository<TDbContext, TEntity> : IReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                                                            where TEntity: class
    {
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public ReadRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public bool Exists(params object[] ids)
        {
            var entity = dbSet.Find(ids);
            return entity != null;
        }

        public Task<bool> ExistsAsync(object[] ids, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.FindAsync(ids, cancellationToken).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });

        public Task<bool> ExistsAsync(params object[] ids)
            => dbSet.FindAsync(ids).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });

        public Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AnyAsync(cancellationToken);

        public Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AnyAsync(predicate, cancellationToken);

        public Task<bool> AllAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.AllAsync(predicate, cancellationToken);

        public Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.CountAsync(cancellationToken);

        public Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.CountAsync(predicate, cancellationToken);

        public Task<long> LongCountAsync(CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.LongCountAsync(cancellationToken);

        public Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken))
            => dbSet.LongCountAsync(predicate, cancellationToken);
    }
}