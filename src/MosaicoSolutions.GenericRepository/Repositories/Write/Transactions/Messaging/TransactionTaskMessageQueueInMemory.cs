using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging
{
    public sealed class TransactionTaskMessageQueueInMemory<TDbContext> : ITransactionTaskMessageQueue<TDbContext> where TDbContext : DbContext
    {
        private readonly Func<TDbContext> _newDbContext;
        private readonly ConcurrentQueue<TransactionTask<TDbContext>> _concurrentQueue;
        
        public TransactionTaskMessageQueueInMemory(Func<TDbContext> newDbContext)
        {
            _newDbContext = newDbContext ?? throw new ArgumentOutOfRangeException(nameof(newDbContext));
            _concurrentQueue = new ConcurrentQueue<TransactionTask<TDbContext>>();
        }

        public void Enqueue(TransactionTask<TDbContext> transactionTask) => _concurrentQueue.Enqueue(transactionTask);

        public event TransactionTaskMessageQueueEventHandle OnSuccess;
        public event TransactionTaskMessageQueueEventHandle OnFailure;
        public event TransactionTaskMessageQueueEventHandle OnComplete;
    }
}