using System;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public sealed class TransactionTaskResult<TDbContext> where TDbContext : DbContext
    {
        internal TransactionTaskResult(Guid transactionId, TransactionTask<TDbContext> transactionTask, Exception exception = null)
        {
            TransactionId = transactionId;
            TransactionTask = transactionTask;
            Exception = exception;
        }

        public Guid TransactionId { get; }
        public TransactionTask<TDbContext> TransactionTask { get; }
        public Exception Exception { get; }
        public bool Success => Exception is null;
        public bool Failure => !Success;
    }
}