using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Read
{
    public class ReadRepository<TDbContext, TEntity> : IReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                                                            where TEntity: class
    {
        protected DbContext dbContext;
        protected DbSet<TEntity> dbSet;

        public ReadRepository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public bool Exists(params object[] ids)
        {
            var entity = dbSet.Find(ids);
            return entity != null;
        }
    }
}