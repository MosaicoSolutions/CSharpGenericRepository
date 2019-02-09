using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces
{
    public interface IReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                          where TEntity: class
    {
        bool Exists(params object[] ids);

        Task<bool> ExistsAsync(object[] ids, CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> ExistsAsync(params object[] ids);

        Task<bool> AnyAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<int> CountAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));

        Task<long> LongCountAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<long> LongCountAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default(CancellationToken));
    }
}