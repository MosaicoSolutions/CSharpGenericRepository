using System.Collections.Generic;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public interface ITransactionalRepository<TEntity> where TEntity : class
    {
        TransactionTask InsertAsTransactionTask(TEntity entity);
        TransactionTask InsertRangeAsTransactionTask(IEnumerable<TEntity> entities);
        TransactionTask InsertRangeAsTransactionTask(params TEntity[] entities);
        TransactionTask UpdateAsTransactionTask(TEntity entity);
        TransactionTask UpdateRangeAsTransactionTask(IEnumerable<TEntity> entities);
        TransactionTask UpdateRangeAsTransactionTask(params TEntity[] entities);
        TransactionTask RemoveAsTransactionTask(TEntity entity);
        TransactionTask RemoveRangeAsTransactionTask(IEnumerable<TEntity> entities);
        TransactionTask RemoveRangeAsTransactionTask(params TEntity[] entities);
        TransactionTask RemoveByIdAsTransactionTask(params object[] keyValues);
    }
}