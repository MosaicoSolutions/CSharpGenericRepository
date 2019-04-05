using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging.Interfaces
{
    public interface ITransactionTaskMessageQueue<TDbContext> where TDbContext : DbContext
    {
        void Enqueue(TransactionTask<TDbContext> transactionTask);
    }
}