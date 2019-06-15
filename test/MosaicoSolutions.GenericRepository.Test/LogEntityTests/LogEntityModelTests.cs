using FluentAssertions;
using MosaicoSolutions.GenericRepository.Data.Entities;
using MosaicoSolutions.GenericRepository.Data.Models;
using MosaicoSolutions.GenericRepository.Repositories.Read;
using MosaicoSolutions.GenericRepository.Repositories.Read.Interfaces;
using MosaicoSolutions.GenericRepository.Repositories.Read.Queries;
using MosaicoSolutions.GenericRepository.Test.Data.Contexts;
using System;
using System.Linq;
using Xunit;

namespace MosaicoSolutions.GenericRepository.Test.LogEntityTests
{
    public class LogEntityModelTests
    {
        protected MarketplaceContext marketplaceContext;
        protected ReadRepository<MarketplaceContext, LogEntity> logEntityReadRepository;

        public LogEntityModelTests()
        {
            marketplaceContext = MarketplaceContext.SqlServerExpress();
            logEntityReadRepository = new DefaultReadRepository<MarketplaceContext, LogEntity>(marketplaceContext);
        }

        [Fact]
        public void ListLogEntityAsLogEntityModel()
        {
            var logEntityModel = logEntityReadRepository.Query(new QueryOptions<LogEntity>
            {
                Where = _ => true
            })
            .ToList()
            .Select(log => new LogEntityModel(log))
            .ToList();

            logEntityModel.ForEach(log =>
            {
                log.EntityType.FullName.Should().Be(log.EntityFullName);
                Guid.TryParse(log.TransactionId, out Guid _).Should().BeTrue();
                log.OriginalValues.GetType().Should().Be(log.EntityType);

                if (log.LogActionType == LogActionType.Insert)
                {
                    log.ChangedValuesAsString.Should().BeNullOrEmpty();
                    log.ModifiedEntityProperties.Any().Should().BeFalse();
                }
                else if (log.LogActionType == LogActionType.Update)
                {
                    log.ChangedValuesAsString.Should().NotBeNullOrEmpty();
                    log.ModifiedEntityProperties.Any().Should().BeTrue();
                }
                else if (log.LogActionType == LogActionType.Delete)
                {
                    log.ChangedValuesAsString.Should().BeNullOrEmpty();
                    log.ModifiedEntityProperties.Any().Should().BeFalse();
                }
            });
        }
    }
}
