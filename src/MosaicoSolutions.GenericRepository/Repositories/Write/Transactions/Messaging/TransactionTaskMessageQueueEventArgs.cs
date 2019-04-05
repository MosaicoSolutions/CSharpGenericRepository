using System;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging
{
    public class TransactionTaskMessageQueueEventArgs : EventArgs
    {
        private readonly object _transactionTask;

        public TransactionTask<TDbContext> TransactionTask<TDbContext>() where TDbContext : DbContext
            => _transactionTask as TransactionTask<TDbContext>;
    }
}