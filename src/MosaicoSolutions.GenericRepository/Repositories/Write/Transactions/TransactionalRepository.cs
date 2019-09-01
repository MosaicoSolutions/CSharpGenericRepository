using System.Collections.Generic;
using System.Linq;
using MosaicoSolutions.GenericRepository.Exceptions;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public class TransactionalRepository<TEntity> : ITransactionalRepository<TEntity> where TEntity : class
    {
        public TransactionTask InsertAsTransactionTask(TEntity entity)
            => new TransactionTask(dbContext =>
            {
                dbContext.Set<TEntity>().Add(entity);
                dbContext.SaveChanges();
            }, nameof(InsertAsTransactionTask));

        public TransactionTask InsertRangeAsTransactionTask(IEnumerable<TEntity> entities)
            => InsertRangeAsTransactionTask(entities.ToArray());

        public TransactionTask InsertRangeAsTransactionTask(params TEntity[] entities)
            => new TransactionTask(dbContext => 
            {
                dbContext.Set<TEntity>().AddRange(entities);
                dbContext.SaveChanges();
            }, nameof(InsertRangeAsTransactionTask));

        public TransactionTask RemoveAsTransactionTask(TEntity entity)
            => new TransactionTask(dbContext => 
            {
                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }, nameof(RemoveAsTransactionTask));

        public TransactionTask RemoveByIdAsTransactionTask(params object[] keyValues)
            => new TransactionTask(dbContext => 
            {
                var entity = dbContext.Set<TEntity>().Find(keyValues);

                if (entity is null)
                    throw new EntityNotFoundForSuppliedKeyValuesException(keyValues);

                dbContext.Set<TEntity>().Remove(entity);
                dbContext.SaveChanges();
            }, nameof(RemoveByIdAsTransactionTask));

        public TransactionTask RemoveRangeAsTransactionTask(IEnumerable<TEntity> entities)
            => RemoveRangeAsTransactionTask(entities.ToArray());

        public TransactionTask RemoveRangeAsTransactionTask(params TEntity[] entities)
            => new TransactionTask(dbContext => 
            {
                dbContext.Set<TEntity>().RemoveRange(entities);
                dbContext.SaveChanges();
            }, nameof(RemoveRangeAsTransactionTask));

        public TransactionTask UpdateAsTransactionTask(TEntity entity)
            => new TransactionTask(dbContext => 
            {
                dbContext.Set<TEntity>().Update(entity);
                dbContext.SaveChanges();
            }, nameof(UpdateAsTransactionTask));

        public TransactionTask UpdateRangeAsTransactionTask(IEnumerable<TEntity> entities)
            => UpdateRangeAsTransactionTask(entities.ToArray());

        public TransactionTask UpdateRangeAsTransactionTask(params TEntity[] entities)
            => new TransactionTask(dbContext => 
            {
                dbContext.Set<TEntity>().UpdateRange(entities);
                dbContext.SaveChanges();
            }, nameof(UpdateRangeAsTransactionTask));
    }
}