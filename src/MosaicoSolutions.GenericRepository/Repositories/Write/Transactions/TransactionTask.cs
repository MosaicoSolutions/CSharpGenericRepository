using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public sealed class TransactionTask
    {
        public TransactionTask(Action<DbContext> action) : this(action, string.Empty)
        { }

        public TransactionTask(Action<DbContext> action, string name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TransactionTaskId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid TransactionTaskId { get; }
        public DateTime CreatedAt { get; }
        public string Name { get; }
        public Action<DbContext> Action { get; }
    }
}