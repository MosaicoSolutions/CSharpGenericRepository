using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    public sealed class QueryBuilder<TEntity> where TEntity : class
    {
        private bool tracking;
        private Expression<Func<TEntity, bool>> where;
        private List<Expression<Func<TEntity, object>>> includes = new List<Expression<Func<TEntity, object>>>();
        private Expression<Func<TEntity, object>> orderBy;
        private SortDirection? direction;
        private List<ThenByOptions<TEntity>> listThenBy = new List<ThenByOptions<TEntity>>();

        public QueryBuilder<TEntity> UseTracking(bool useTracking = true)
            => ReturnThis(() => tracking = useTracking);

        public QueryBuilder<TEntity> Where(Expression<Func<TEntity, bool>> predicate) 
            => ReturnThis(() => where = predicate);

        public QueryBuilder<TEntity> Include(Expression<Func<TEntity, object>> include)
            => ReturnThis(() => includes.Add(include));

        public QueryBuilder<TEntity> OrderBy(Expression<Func<TEntity, object>> orderBy)
            => ReturnThis(() => 
            {
                this.orderBy = orderBy;
                this.direction = Queries.SortDirection.Ascending;
            });

        public QueryBuilder<TEntity> OrderByDescending(Expression<Func<TEntity, object>> orderBy)
            => ReturnThis(() => 
            {
                this.orderBy = orderBy;
                this.direction = Queries.SortDirection.Descending;
            });

        public QueryBuilder<TEntity> ThenBy(Expression<Func<TEntity, object>> thenBy)
            => ReturnThis(() => listThenBy.Add(new ThenByOptions<TEntity>
            {
                Direction = Queries.SortDirection.Ascending,
                OrderBy = thenBy
            }));

        public QueryBuilder<TEntity> ThenByDescending(Expression<Func<TEntity, object>> thenBy)
            => ReturnThis(() => listThenBy.Add(new ThenByOptions<TEntity>
            {
                Direction = Queries.SortDirection.Descending,
                OrderBy = thenBy
            }));

        public QueryOptions<TEntity> Build()
        {
            var queryOptions = new QueryOptions<TEntity>
            {
                Tracking = tracking,
                Where = where,
                Includes = includes.ToArray(),
                Sort = new SortOptions<TEntity>
                {
                    Direction = direction,
                    OrderBy = orderBy,
                    ThenByCollection = listThenBy.ToArray()
                }
            };

            return queryOptions;
        }

        private QueryBuilder<TEntity> ReturnThis(Action action)
        {
            action();
            return this;
        }
    }

    public static class QueryBuilder
    {
        public static QueryBuilder<TEntity> For<TEntity>() where TEntity : class
            => new QueryBuilder<TEntity>();
    }
}