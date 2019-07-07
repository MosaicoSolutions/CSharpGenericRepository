using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    class LogEntityConfigurationDbOptionsExtensions : IDbContextOptionsExtension
    {
        internal object LogEntityConfiguration { get; }

        internal LogEntityConfigurationDbOptionsExtensions(object logEntityConfiguration) => LogEntityConfiguration = logEntityConfiguration;

        public string LogFragment => $"LogEntityConfigurationType[{LogEntityConfiguration.GetType()}]";

        public bool ApplyServices(IServiceCollection services)
            => true;

        public long GetServiceProviderHashCode()
            => GetHashCode();

        public void Validate(IDbContextOptions options)
        { }
    }
}
