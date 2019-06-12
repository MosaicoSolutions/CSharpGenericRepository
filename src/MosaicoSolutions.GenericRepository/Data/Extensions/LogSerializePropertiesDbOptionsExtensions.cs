using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace MosaicoSolutions.GenericRepository.Data.Extensions
{
    class LogSerializePropertiesDbOptionsExtensions : IDbContextOptionsExtension
    {
        internal Type typeLogSerializeProperties;

        internal LogSerializePropertiesDbOptionsExtensions(Type typeLogSerializeProperties)
        {
            this.typeLogSerializeProperties = typeLogSerializeProperties;
        }

        public string LogFragment => throw new NotImplementedException();

        public bool ApplyServices(IServiceCollection services)
            => true;

        public long GetServiceProviderHashCode()
            => GetHashCode();

        public void Validate(IDbContextOptions options)
        {
        }
    }
}
