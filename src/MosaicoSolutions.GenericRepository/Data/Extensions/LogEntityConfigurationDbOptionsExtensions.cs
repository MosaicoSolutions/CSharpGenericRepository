using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    class LogEntityConfigurationDbOptionsExtensions : IDbContextOptionsExtension 
    {
        internal object logEntityConfiguration;

        internal LogEntityConfigurationDbOptionsExtensions(object logEntityConfiguration)
        {
            this.logEntityConfiguration = logEntityConfiguration;
        }

        public string LogFragment => $"LogEntityConfigurationType[{logEntityConfiguration.GetType()}]";

        public bool ApplyServices(IServiceCollection services)
            => true;

        public long GetServiceProviderHashCode()
            => GetHashCode();

        public void Validate(IDbContextOptions options)
        {
        }
    }
}
