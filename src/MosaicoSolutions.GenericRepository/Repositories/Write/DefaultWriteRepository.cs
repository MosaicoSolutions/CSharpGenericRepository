using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Write.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Write
{
    public class DefaultWriteRepository<TDbContext, TEntity> : WriteRepository<TDbContext, TEntity> where TDbContext : DbContext
                                                                                                    where TEntity : class
    {
        public DefaultWriteRepository(TDbContext dbContext) : base(dbContext)
        {}
    }
}