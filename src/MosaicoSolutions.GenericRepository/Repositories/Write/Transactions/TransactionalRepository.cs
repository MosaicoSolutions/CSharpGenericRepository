using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public abstract class TransactionalRepository<TDbContext> where TDbContext : DbContext
    { }

    public abstract class TransactionalRepository<TDbContext, TEntity> : TransactionalRepository<TDbContext> where TDbContext : DbContext
                                                                                                             where TEntity : class
    {
        // public virtual TransactionTask<TDbContext> InsertAsTransactionTask(TEntity entity)
        //     => dbContext => 
        //     {
        //         dbContext.Entry(entity).State = EntityState.Added;
        //         dbContext.SaveChanges();
        //     };

        // public virtual TransactionTask<TDbContext> InsertRangeAsTransactionTask(IEnumerable<TEntity> entities)
        //     => dbContext => 
        //     {
        //         dbContext.Set<TEntity>().AddRange(entities);
        //         dbContext.SaveChanges();
        //     };

        // public virtual TransactionTask<TDbContext> InsertRangeAsTransactionTask(params TEntity[] entities)
        //     => dbContext => 
        //     {
        //         dbContext.Set<TEntity>().AddRange(entities);
        //         dbContext.SaveChanges();
        //     };

        // public virtual TransactionTask<TDbContext> Update(TEntity entity)
        //     => dbContext =>
        //     {
        //         dbContext.Set<TEntity>().Update(entity);
        //         dbContext.SaveChanges();
        //     };

        // public virtual TransactionTask<TDbContext> UpdateRange(IEnumerable<TEntity> entities)
        //     => dbContext =>
        //     {
        //         dbContext.Set<TEntity>().UpdateRange(entities);
        //         dbContext.SaveChanges();
        //     };

        // public virtual TransactionTask<TDbContext> UpdateRange(params TEntity[] entities)
        //     => dbContext =>
        //     {
        //         dbContext.Set<TEntity>().UpdateRange(entities);
        //         dbContext.SaveChanges();
        //     };
    }
}