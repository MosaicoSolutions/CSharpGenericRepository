using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Manager.Interfaces
{
    public interface ITransactionTaskManager
    {
        TransactionTaskResult UseTransaction(TransactionTask transactionTask);
    }
}