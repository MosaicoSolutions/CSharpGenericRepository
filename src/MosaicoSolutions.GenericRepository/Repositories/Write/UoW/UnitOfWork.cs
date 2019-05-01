using System;
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

        public Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken)) => dbContext.SaveChangesAsync(cancellationToken);
    }
}