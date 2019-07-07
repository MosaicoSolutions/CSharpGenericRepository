using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Queries
{
    public interface IQuery<TEntity> where TEntity : class
    {
        TEntity First();
        Task<TEntity> FirstAsync(CancellationToken cancellationToken = default);
        TEntity FirstOrDefault();
        Task<TEntity> FirstOrDefaultAsync(CancellationToken cancellationToken = default);
        List<TEntity> ToList();
        Task<List<TEntity>> ToListAsync(CancellationToken cancellationToken = default);
        TEntity Last();
        Task<TEntity> LastAsync(CancellationToken cancellationToken = default);
        TEntity LastOrDefault();
        Task<TEntity> LastOrDefaultAsync(CancellationToken cancellationToken = default);
        TEntity Single();
        Task<TEntity> SingleAsync(CancellationToken cancellationToken = default);
        TEntity SingleOrDefault();
        Task<TEntity> SingleOrDefaultAsync(CancellationToken cancellationToken = default);
    }
}