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
    }
}