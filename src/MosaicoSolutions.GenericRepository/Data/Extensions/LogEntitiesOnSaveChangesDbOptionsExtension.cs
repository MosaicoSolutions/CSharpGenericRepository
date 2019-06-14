using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    class LogEntitiesOnSaveChangesDbOptionsExtension : IDbContextOptionsExtension
    {
        public bool LogEntitiesOnSave { get; }

        public LogEntitiesOnSaveChangesDbOptionsExtension(bool logEntitiesOnSave) => LogEntitiesOnSave = logEntitiesOnSave;

        public string LogFragment => $"{nameof(LogEntitiesOnSave)}[{LogEntitiesOnSave}]";

        public bool ApplyServices(IServiceCollection services)
            => true;

        public long GetServiceProviderHashCode()
            => base.GetHashCode();

        public void Validate(IDbContextOptions options)
        { }
    }
}
