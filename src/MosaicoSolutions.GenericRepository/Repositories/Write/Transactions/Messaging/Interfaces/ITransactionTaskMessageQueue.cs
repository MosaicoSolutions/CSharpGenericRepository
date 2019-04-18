using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions.Messaging.Interfaces
{
    public interface ITransactionTaskMessageQueue
    {
        void Enqueue(TransactionTask transactionTask);
    }
}