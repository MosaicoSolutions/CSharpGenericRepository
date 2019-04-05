using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager
{
    public class TransactionTaskManager<TDbContext> : ITransactionTaskManager<TDbContext> where TDbContext : DbContext
    {
        private readonly Func<TDbContext> _newDbContext;

        public TransactionTaskManager(Func<TDbContext> newDbContext)
        {
            _newDbContext = newDbContext ?? throw new ArgumentOutOfRangeException(nameof(newDbContext));
        }

        public TransactionTaskResult<TDbContext> UseTransaction(TransactionTask<TDbContext> transactionTask)
            => transactionTask is null
                ? throw new ArgumentNullException(nameof(transactionTask))
                : TryUseTransaction(transactionTask);

        private TransactionTaskResult<TDbContext> TryUseTransaction(TransactionTask<TDbContext> transactionTask)
        {
            using (var dbContext = _newDbContext())
                using (var transaction = dbContext.Database.BeginTransaction())
                    try
                    {
                        transactionTask.Action(dbContext);
                        dbContext.SaveChanges();

                        transaction.Commit();

                        var transactionTaskSuccess = new TransactionTaskResult<TDbContext>(transaction.TransactionId, transactionTask);
                        return transactionTaskSuccess;
                    }
                    catch(Exception e)
                    {
                        var transactionTaskFailure = new TransactionTaskResult<TDbContext>(transaction.TransactionId, transactionTask, e);
                        return transactionTaskFailure;
                    }
        }
    }
}