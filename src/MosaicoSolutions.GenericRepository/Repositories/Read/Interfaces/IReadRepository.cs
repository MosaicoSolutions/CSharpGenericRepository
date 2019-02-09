using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces
{
    public interface IReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                          where TEntity: class
    {
        bool Exists(params object[] ids);
    }
}