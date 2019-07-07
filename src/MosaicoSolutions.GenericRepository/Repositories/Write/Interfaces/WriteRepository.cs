using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Exceptions;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Interfaces
{
    public abstract class WriteRepository<TDbContext, TEntity> where TDbContext : DbContext
                                                               where TEntity : class
    {
        public TDbContext DbContext { get; }
        public DbSet<TEntity> DbSet { get; set; }

        public WriteRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity) => DbSet.Add(entity);

        public virtual Task InsertAsync(TEntity entity) => DbSet.AddAsync(entity);

        public virtual void InsertRange(IEnumerable<TEntity> entities) => DbSet.AddRange(entities);

        public virtual void InsertRange(params TEntity[] entities) => DbSet.AddRange(entities);

        public virtual Task InsertRangeAsync(IEnumerable<TEntity> entities) => DbSet.AddRangeAsync(entities);

        public virtual Task InsertRangeAsync(params TEntity[] entities) => DbSet.AddRangeAsync(entities);

        public virtual void Update(TEntity entity) => DbSet.Update(entity);

        public virtual void UpdateRange(IEnumerable<TEntity> entities) => DbSet.UpdateRange(entities);

        public virtual void UpdateRange(params TEntity[] entities) => DbSet.UpdateRange(entities);

        public virtual void Remove(TEntity entity) => DbSet.Remove(entity);

        public virtual void RemoveRange(IEnumerable<TEntity> entities) => DbSet.RemoveRange(entities);

        public virtual void RemoveRange(params TEntity[] entities) => DbSet.RemoveRange(entities);

        public virtual void RemoveById(params object[] keyValues)
        {
            var entity = DbSet.Find(keyValues);

            if (entity is null)
                throw new EntityNotFoundForSuppliedKeyValuesException(keyValues);

            this.Remove(entity);
        }

        public virtual int RemoveWhere(Expression<Func<TEntity, bool>> predicate)
        {
            var entities = DbSet.Where(predicate).ToList();

            this.RemoveRange(entities);

            return entities.Count;
        }
    }
}