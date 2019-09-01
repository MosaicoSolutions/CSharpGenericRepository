using System;
using System.Linq;
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
                                                               CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                              where TEntity : class
            => @this.DbSet.AnyAsync(cancellationToken);

        public static bool Any<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this) where TDbContext : DbContext
                                                                                                    where TEntity : class
            => @this.DbSet.Any();

        public static Task<bool> AnyAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, bool>> predicate,
                                                               CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                              where TEntity : class
            => @this.DbSet.AnyAsync(predicate, cancellationToken);

        public static bool Any<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, bool>> predicate) where TDbContext : DbContext
                                                                                                          where TEntity : class
            => @this.DbSet.Any(predicate);

        public static Task<bool> AllAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, bool>> predicate,
                                                               CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                              where TEntity : class
            => @this.DbSet.AllAsync(predicate, cancellationToken);

        public static bool All<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                    Expression<Func<TEntity, bool>> predicate) where TDbContext : DbContext
                                                                                               where TEntity : class
            => @this.DbSet.All(predicate);

        public static Task<int> CountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                               where TEntity : class
            => @this.DbSet.CountAsync(cancellationToken);

        public static int Count<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this) where TDbContext : DbContext
                                                                                                     where TEntity : class
            => @this.DbSet.Count();

        public static Task<int> CountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, bool>> predicate,
                                                                CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                               where TEntity : class
            => @this.DbSet.CountAsync(predicate, cancellationToken);

        public static int Count<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, bool>> predicate) where TDbContext : DbContext
                                                                                                           where TEntity : class
            => @this.DbSet.Count(predicate);

        public static Task<long> LongCountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                   where TEntity : class
            => @this.DbSet.LongCountAsync(cancellationToken);

        public static long LongCount<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this) where TDbContext : DbContext
                                                                                                          where TEntity : class
            => @this.DbSet.LongCount();

        public static Task<long> LongCountAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, bool>> predicate,
                                                                     CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                    where TEntity : class
            => @this.DbSet.LongCountAsync(predicate, cancellationToken);

        public static long LongCount<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, bool>> predicate) where TDbContext : DbContext
                                                                                                                where TEntity : class
            => @this.DbSet.LongCount(predicate);

        public static Task<TEntity> MinAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                 where TEntity : class
            => @this.DbSet.MinAsync(cancellationToken);

        public static TEntity Min<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this) where TDbContext : DbContext
                                                                                                       where TEntity : class
            => @this.DbSet.Min();

        public static Task<TResult> MinAsync<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                           Expression<Func<TEntity, TResult>> selector,
                                                                           CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                          where TEntity : class
            => @this.DbSet.MinAsync(selector, cancellationToken);

        public static TResult Min<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                           Expression<Func<TEntity, TResult>> selector) where TDbContext : DbContext
                                                                                                                        where TEntity : class
            => @this.DbSet.Min(selector);

        public static Task<TEntity> MaxAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                 where TEntity : class
            => @this.DbSet.MaxAsync(cancellationToken);

        public static TEntity Max<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this) where TDbContext : DbContext
                                                                                                       where TEntity : class
            => @this.DbSet.Max();

        public static Task<TResult> MaxAsync<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                           Expression<Func<TEntity, TResult>> selector,
                                                                           CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                          where TEntity : class
            => @this.DbSet.MaxAsync(selector, cancellationToken);

        public static TResult Max<TDbContext, TEntity, TResult>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, TResult>> selector) where TDbContext : DbContext
                                                                                                             where TEntity : class
            => @this.DbSet.Max(selector);

        public static Task<decimal> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, decimal>> selector,
                                                                  CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                 where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static decimal Sum<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                       Expression<Func<TEntity, decimal>> selector) where TDbContext : DbContext
                                                                                                    where TEntity : class
            => @this.DbSet.Sum(selector);

        public static Task<decimal?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, decimal?>> selector,
                                                                  CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                 where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static decimal? Sum<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                        Expression<Func<TEntity, decimal?>> selector) where TDbContext : DbContext
                                                                                                      where TEntity : class
            => @this.DbSet.Sum(selector);

        public static Task<int> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                              Expression<Func<TEntity, int>> selector,
                                                              CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                             where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static int Sum<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                   Expression<Func<TEntity, int>> selector) where TDbContext : DbContext
                                                                                            where TEntity : class
            => @this.DbSet.Sum(selector);

        public static Task<int?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, int?>> selector,
                                                               CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                              where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<long> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                               Expression<Func<TEntity, long>> selector,
                                                               CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                 where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<long?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, long?>> selector,
                                                                CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                  where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<double> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                 Expression<Func<TEntity, double>> selector,
                                                                 CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                   where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<double?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                  Expression<Func<TEntity, double?>> selector,
                                                                  CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                    where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<float> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                Expression<Func<TEntity, float>> selector,
                                                                CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                  where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<float?> SumAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                 Expression<Func<TEntity, float?>> selector,
                                                                 CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                   where TEntity : class
            => @this.DbSet.SumAsync(selector, cancellationToken);

        public static Task<decimal> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, decimal>> selector,
                                                                      CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                        where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<decimal?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                       Expression<Func<TEntity, decimal?>> selector,
                                                                       CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                         where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, int>> selector,
                                                                     CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                       where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, int?>> selector,
                                                                      CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                        where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, long>> selector,
                                                                     CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                       where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, long?>> selector,
                                                                      CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                        where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    Expression<Func<TEntity, double>> selector,
                                                                    CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                      where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<double?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                      Expression<Func<TEntity, double?>> selector,
                                                                      CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                        where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<float> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    Expression<Func<TEntity, float>> selector,
                                                                    CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                      where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<float?> AverageAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                     Expression<Func<TEntity, float?>> selector,
                                                                     CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                       where TEntity : class
            => @this.DbSet.AverageAsync(selector, cancellationToken);

        public static Task<bool> ContainsAsync<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                                    TEntity entity,
                                                                    CancellationToken cancellationToken = default) where TDbContext : DbContext
                                                                                                                                      where TEntity : class
            => @this.DbSet.ContainsAsync(entity, cancellationToken);

        public static bool Contains<TDbContext, TEntity>(this ReadRepository<TDbContext, TEntity> @this,
                                                         TEntity entity) where TDbContext : DbContext
                                                                                            where TEntity : class
            => @this.DbSet.Contains(entity);
    }
}