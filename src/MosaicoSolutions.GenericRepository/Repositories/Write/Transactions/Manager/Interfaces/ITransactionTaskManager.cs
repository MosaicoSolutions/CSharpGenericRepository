using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager.Interfaces
{
    public interface ITransactionTaskManager<TDbContext> where TDbContext : DbContext
    {
        TransactionTaskResult UseTransaction(TransactionTask transactionTask);
    }
}