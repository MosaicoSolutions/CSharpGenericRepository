using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    public interface IQuery<TEntity> where TEntity : class
    {
        TEntity First();
        Task<TEntity> FirstAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
        List<TEntity> ToList();
        Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity Last();
        Task<TEntity> LastAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity LastOrDefault();
        Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity Single();
        Task<TEntity> SingleAsync(CancellationToken cancellationToken = default(CancellationToken));
        TEntity SingleOrDefault();
        Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}