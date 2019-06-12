using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MosaicoSolutions.GenericRepository.Annotations;
using MosaicoSolutions.GenericRepository.Data.Entities;
using MosaicoSolutions.GenericRepository.Data.EntitiesConfiguration;
using MosaicoSolutions.GenericRepository.Data.Extensions;
using MosaicoSolutions.GenericRepository.Data.Serialization.Json;
using Newtonsoft.Json;
using System;
using System.Linq;

namespace MosaicoSolutions.GenericRepository.Data.Contexts
{
    public class LogWriteDbContext : WriteDbContext
    {
        private readonly EntityState[] entityStatesToLog;
        private readonly IEntityTypeConfiguration<LogEntity> logEntityConfiguration;

        public LogWriteDbContext(DbContextOptions options) : base(options)
        {
            var logEntityConfigurationExtension = options.FindExtension<LogEntityConfigurationDbOptionsExtensions>();
            logEntityConfiguration = logEntityConfigurationExtension?.logEntityConfiguration as IEntityTypeConfiguration<LogEntity> ?? new LogEntityConfiguration();

            entityStatesToLog = new EntityState[]
            {
                EntityState.Added,
                EntityState.Deleted,
                EntityState.Modified
            };
        }

        protected LogWriteDbContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(logEntityConfiguration);
            base.OnModelCreating(modelBuilder);
        }

        protected override void BeforeSaveChanges()
        {
            base.BeforeSaveChanges();
            LogEntities();
        }

        private void LogEntities()
        {
            var transactionId = Database.CurrentTransaction?.TransactionId ?? Guid.NewGuid();
            var transactionIdWasGenerated = Database.CurrentTransaction is null;

            var logs = ChangeTracker.Entries().Where(MustLogEntity)
                                              .AsParallel()
                                              .Select(entityEntry =>
                                              {
                                                  switch (entityEntry.State)
                                                  {
                                                      case EntityState.Added:
                                                          return LogAddedEntity(entityEntry, transactionId.ToString(), transactionIdWasGenerated);
                                                      case EntityState.Modified:
                                                          return LogModifiedEntity(entityEntry);
                                                      default:
                                                          return LogDeletedEntity(entityEntry);
                                                  }
                                              })
                                              .ToList();

            Set<LogEntity>().AddRange(logs);
        }

        private bool MustLogEntity(EntityEntry entityEntry)
        {
            if (!entityEntry.Entity.GetType().IsDefined(typeof(EntityLogAttribute), false))
                return entityStatesToLog.Contains(entityEntry.State);

            var ignoreEntity = entityEntry.Entity.GetType().GetCustomAttributes(false)
                                                           .OfType<EntityLogAttribute>()
                                                           .Select(a => a.IgnoreEntity)
                                                           .FirstOrDefault();

            return !ignoreEntity && entityStatesToLog.Contains(entityEntry.State);
        }

        private LogEntity LogAddedEntity(EntityEntry entityEntry, string transactionId, bool transactionIdWasGenerated)
        {
            var logEntity = new LogEntity
            {
                EntityName = entityEntry.Entity.GetType().Name,
                LogActionType = EntityStateToLogActionType(entityEntry.State),
                OriginalValues = JsonConvert.SerializeObject(entityEntry.Entity, new JsonSerializerSettings
                {
                    ContractResolver = new SimpleTypeContractResolver()
                }),
                CreatedAt = DateTime.Now,
                TransactionId = transactionId,
                TransactionIdWasGenerated = transactionIdWasGenerated
            };

            return logEntity;
        }

        private LogEntity LogModifiedEntity(EntityEntry entityEntry)
        {
            throw new NotImplementedException();
        }

        private LogEntity LogDeletedEntity(EntityEntry entityEntry)
        {
            throw new NotImplementedException();
        }

        LogActionType EntityStateToLogActionType(EntityState entityState)
            => entityState == EntityState.Added
                    ? LogActionType.Insert
                    : entityState == EntityState.Modified
                        ? LogActionType.Update
                        : LogActionType.Delete;
    }
}