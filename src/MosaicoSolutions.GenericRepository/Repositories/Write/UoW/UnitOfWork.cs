using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.UoW
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext: DbContext
    {
        private TDbContext dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit() => dbContext.SaveChanges();

        public Task<int> CommitAsync(CancellationToken cancellationToken = default) => dbContext.SaveChangesAsync(cancellationToken);

        public void RejectChanges()
        {
            foreach (var entry in dbContext.ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
        }
    }
}