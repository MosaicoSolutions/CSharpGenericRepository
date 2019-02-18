using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Extensions
{
    public static class ReadRepositoryQueryableExtensions
    {
        public static Task<bool> AnyAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                               CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                 where TEntity: class
            => @this.DbSet.AnyAsync(cancellationToken);

        public static Task<bool> AnyAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                               Expression<Func<TEntity, bool>> predicate, 
                                                               CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                 where TEntity: class
            => @this.DbSet.AnyAsync(predicate, cancellationToken);

        public static Task<bool> AllAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                               Expression<Func<TEntity, bool>> predicate, 
                                                               CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                 where TEntity: class
            => @this.DbSet.AllAsync(predicate, cancellationToken);

        public static Task<int> CountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                                CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                  where TEntity: class
            => @this.DbSet.CountAsync(cancellationToken);

        public static Task<int> CountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                                Expression<Func<TEntity, bool>> predicate, 
                                                                CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                  where TEntity: class
            => @this.DbSet.CountAsync(predicate, cancellationToken);

        public static Task<long> LongCountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                                    CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                      where TEntity: class
            => @this.DbSet.LongCountAsync(cancellationToken);

        public static Task<long> LongCountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this, 
                                                                     Expression<Func<TEntity, bool>> predicate, 
                                                                     CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                       where TEntity: class
            => @this.DbSet.LongCountAsync(predicate, cancellationToken);
    }
}