using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;

namespace MosaicoSolutions.GenericRepository.Repositories.Read
{
    public class DefaultReadRepository<TDbContext, TEntity> : ReadRepository<TDbContext, TEntity> where TDbContext: DbContext
                                                                                                  where TEntity: class
    {
        public DefaultReadRepository(TDbContext dbContext) : base(dbContext)
        { }
    }
}