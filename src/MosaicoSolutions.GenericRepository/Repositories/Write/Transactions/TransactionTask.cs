using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.Transactions
{
    public sealed class TransactionTask<TDbContext> where TDbContext: DbContext
    {
        public TransactionTask(Action<TDbContext> action) : this(action, string.Empty)
        { }

        public TransactionTask(Action<TDbContext> action, string name)
        {
            Action = action ?? throw new ArgumentNullException(nameof(action));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            TransactionTaskId = Guid.NewGuid();
            CreatedAt = DateTime.Now;
        }

        public Guid TransactionTaskId { get; }
        public DateTime CreatedAt { get; }
        public string Name { get; set; }
        public Action<TDbContext> Action { get; }
    }
}