using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using MosaicoSolutions.GenericRepository.Data.Entities;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    public static class DbContextOptionsBuilderExtensions
    {
        public static DbContextOptionsBuilder UseLogEntityConfiguration<TLogEntityConfiguration>(this DbContextOptionsBuilder @this, 
                                                                                                 TLogEntityConfiguration logEntityConfiguration)
            where TLogEntityConfiguration : IEntityTypeConfiguration<LogEntity>
        {
            ((IDbContextOptionsBuilderInfrastructure)@this).AddOrUpdateExtension(new LogEntityConfigurationDbOptionsExtensions(logEntityConfiguration));
            return @this;
        }
    }
}
