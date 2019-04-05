using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.UoW
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext: DbContext
    {
        private TDbContext dbContext;
        private bool disposed;

        public UnitOfWork(TDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public int Commit() => dbContext.SaveChanges();

        public Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken)) => dbContext.SaveChangesAsync(cancellationToken);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed) return;
 
            if (disposing)
                dbContext.Dispose();
            
            disposed = true;
        }
    }
}