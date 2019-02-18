using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces
{
    public abstract class ReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                              where TEntity: class
    {
        public TDbContext DbContext { get; set; }
        public DbSet<TEntity> DbSet { get; set; }

        public ReadRepository(TDbContext dbContext)
        {
            DbContext = dbContext;
            DbSet = dbContext.Set<TEntity>();
        }

        public bool Exists(params object[] ids)
        {
            var entity = DbSet.Find(ids);
            return entity != null;
        }

        public Task<bool> ExistsAsync(object[] ids, CancellationToken cancellationToken = default(CancellationToken))
            => DbSet.FindAsync(ids, cancellationToken).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });

        public Task<bool> ExistsAsync(params object[] ids)
            => DbSet.FindAsync(ids).ContinueWith(task => 
            {
                task.Wait();
                return task.Result != null;
            });
    }
}