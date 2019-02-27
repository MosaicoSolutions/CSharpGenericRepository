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

        public static Task<TEntity> MinAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                    where TEntity: class
            => @this.DbSet.MinAsync(cancellationToken);

        public static Task<TResult> MinAsync<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                           Expression<Func<TEntity, TResult>> selector,
                                                                           CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                             where TEntity: class
            => @this.DbSet.MinAsync(selector, cancellationToken);

        public static Task<TEntity> MaxAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                    where TEntity: class
            => @this.DbSet.MaxAsync(cancellationToken);

        public static Task<TResult> MaxAsync<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                           Expression<Func<TEntity, TResult>> selector,
                                                                           CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                             where TEntity: class
            => @this.DbSet.MaxAsync(selector, cancellationToken);

        public static Task<decimal> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, decimal>> selector,
                                                                  CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                    where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<decimal?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, decimal?>> selector,
                                                                  CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                    where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);
        
        public static Task<int> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                              Expression<Func<TEntity, int>> selector,
                                                              CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<int?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, int?>> selector,
                                                               CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                 where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<long> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, long>> selector,
                                                               CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                 where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<long?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, long?>> selector,
                                                                CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                  where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<double> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                 Expression<Func<TEntity, double>> selector,
                                                                 CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                   where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<double?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, double?>> selector,
                                                                  CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                    where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<float> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, float>> selector,
                                                                CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                  where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<float?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                 Expression<Func<TEntity, float?>> selector,
                                                                 CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                   where TEntity: class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<decimal> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, decimal>> selector,
                                                                      CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                        where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<decimal?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                       Expression<Func<TEntity, decimal?>> selector,
                                                                       CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                         where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, int>> selector,
                                                                     CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                       where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, int?>> selector,
                                                                      CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                        where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, long>> selector,
                                                                     CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                       where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, long?>> selector,
                                                                      CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                        where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    Expression<Func<TEntity, double>> selector,
                                                                    CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                      where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, double?>> selector,
                                                                      CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                        where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<float> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    Expression<Func<TEntity, float>> selector,
                                                                    CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                      where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<float?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, float?>> selector,
                                                                     CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                       where TEntity: class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<bool> ContainsAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    TEntity entity,
                                                                    CancellationToken cancellationToken = default(CancellationToken)) where TDbContext: DbContext
                                                                                                                                      where TEntity: class
            => @this.DbSet.ContainsAsync(entity, cancellationToken);
    }
}