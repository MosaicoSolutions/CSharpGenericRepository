using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Read
{
    public class DefaultReadRepository<TDbContext, TEntity> : ReadRepository<TDbContext, TEntity> where TDbContext : DbContext
                                                                                                  where TEntity : class
    {
        public DefaultReadRepository(TDbContext dbContext) : base(dbContext)
        { }
    }
}