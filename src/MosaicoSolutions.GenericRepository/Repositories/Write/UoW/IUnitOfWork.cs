using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Write.UoW
{
    public interface IUnitOfWork<TDbContext> : IDisposable where TDbContext: DbContext
    {
        int Commit();

        Task<int> CommitAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}