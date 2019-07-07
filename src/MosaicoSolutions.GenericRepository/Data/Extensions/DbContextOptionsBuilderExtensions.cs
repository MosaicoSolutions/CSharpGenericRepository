using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MosaicoSolutions.GenericRepository.Data.Contexts;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder<TDbContext> UseLogEntitiesOnSaveChanges<TDbContext>(this DbContextOptionsBuilder<TDbContext> @this,
                                                                                                  bool logEntitiesOnSave = true) where TDbContext : WriteDbContext
        {
            ((IDbContextOptionsBuilderInfrastructure)@this).AddOrUpdateExtension(new LogEntitiesOnSaveChangesDbOptionsExtension(logEntitiesOnSave));
            return @this;
        }

        public static DbContextOptionsBuilder UseLogEntityConfiguration<TDbContext, TLogEntityConfiguration>(this DbContextOptionsBuilder<TDbContext> @this,
                                                                                                             TLogEntityConfiguration logEntityConfiguration)
            where TDbContext : WriteDbContext
            where TLogEntityConfiguration : IEntityTypeConfiguration<LogEntity>
        {
            ((IDbContextOptionsBuilderInfrastructure)@this).AddOrUpdateExtension(new LogEntityConfigurationDbOptionsExtensions(logEntityConfiguration));
            return @this;
        }
    }
}
