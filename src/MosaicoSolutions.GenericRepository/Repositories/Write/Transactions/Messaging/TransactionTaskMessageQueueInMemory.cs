using System;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging
{
    public sealed class TransactionTaskMessageQueueInMemory : ITransactionTaskMessageQueue
    {
        private readonly Func<DbContext> _newDbContext;
        private readonly ConcurrentQueue<TransactionTask> _concurrentQueue;
        
        public TransactionTaskMessageQueueInMemory(Func<DbContext> newDbContext)
        {
            _newDbContext = newDbContext ?? throw new ArgumentOutOfRangeException(nameof(newDbContext));
            _concurrentQueue = new ConcurrentQueue<TransactionTask>();
        }

        public void Enqueue(TransactionTask transactionTask) => _concurrentQueue.Enqueue(transactionTask);

        public event TransactionTaskMessageQueueEventHandle OnSuccess;
        public event TransactionTaskMessageQueueEventHandle OnFailure;
        public event TransactionTaskMessageQueueEventHandle OnComplete;
    }
}