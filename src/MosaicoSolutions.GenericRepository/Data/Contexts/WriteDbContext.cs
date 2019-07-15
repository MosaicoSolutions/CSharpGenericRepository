using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MosaicoSolutions.GenericRepository.Annotations;
using MosaicoSolutions.GenericRepository.Data.Entities;
using MosaicoSolutions.GenericRepository.Data.EntitiesConfiguration;
using MosaicoSolutions.GenericRepository.Data.Extensions;
using MosaicoSolutions.GenericRepository.Data.Models;
using MosaicoSolutions.GenericRepository.Data.Serialization.Json;
using Newtonsoft.Json;

namespace MosaicoSolutions.GenericRepository.Data.Contexts
{
    public class WriteDbContext : DbContext
    {
        private readonly EntityState[] entityStatesToLog = new EntityState[]
        {
            EntityState.Added,
            EntityState.Deleted,
            EntityState.Modified
        };

        public IEntityTypeConfiguration<LogEntity> LogEntityTypeConfiguration { get; }
        public bool LogEntitiesOnSave { get; }

        public WriteDbContext(DbContextOptions options) : base(options)
        {
            LogEntitiesOnSave = options.FindExtension<LogEntitiesOnSaveChangesDbOptionsExtension>()?.LogEntitiesOnSave ?? false;
            LogEntityTypeConfiguration = options.FindExtension<LogEntityConfigurationDbOptionsExtensions>()?
                                                .LogEntityConfiguration as IEntityTypeConfiguration<LogEntity> ?? new LogEntityConfiguration();
        }

        protected WriteDbContext()
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (LogEntitiesOnSave)
                modelBuilder.ApplyConfiguration(LogEntityTypeConfiguration);

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
            => this.SaveChanges(acceptAllChangesOnSuccess: true);

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            BeforeSaveChanges();
            return LogEntitiesOnSave && Database.CurrentTransaction != null
                    ? LogEntities(() => base.SaveChanges(acceptAllChangesOnSuccess))
                    : base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            => this.SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken: cancellationToken);

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            BeforeSaveChanges();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        protected virtual void BeforeSaveChanges()
        {
            CreatedAtAttribute();
            LastUpdatedAtAttribute();
            RowVersionAttribute();
        }

        private void CreatedAtAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(CreatedAtAttribute), true) && p.PropertyType == typeof(DateTime))))
            {
                var property = entry.Entity.GetType()
                                           .GetProperties()
                                           .FirstOrDefault(p => p.IsDefined(typeof(CreatedAtAttribute), true) && p.PropertyType == typeof(DateTime));

                var propertyName = property?.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        var useUtc = property?.GetCustomAttributes(true)
                                              .OfType<CreatedAtAttribute>()
                                              .Select(a => a.UseUtc)
                                              .FirstOrDefault() ?? false;
                        entry.Property(propertyName).CurrentValue = useUtc ? DateTime.UtcNow : DateTime.Now;
                        break;

                    case EntityState.Modified:
                        entry.Property(propertyName).IsModified = false;
                        break;
                }
            }
        }

        private void LastUpdatedAtAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(LastUpdatedAtAttribute), true) && p.PropertyType == typeof(DateTime?))))
            {
                var property = entry.Entity.GetType()
                                           .GetProperties()
                                           .FirstOrDefault(p => p.IsDefined(typeof(LastUpdatedAtAttribute), true) && p.PropertyType == typeof(DateTime?));

                var propertyName = property?.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(propertyName).CurrentValue = null;
                        break;

                    case EntityState.Modified:
                        var useUtc = property?.GetCustomAttributes(true)
                                              .OfType<LastUpdatedAtAttribute>()
                                              .Select(a => a.UseUtc)
                                              .FirstOrDefault() ?? false;
                        entry.Property(propertyName).CurrentValue = useUtc ? DateTime.UtcNow : DateTime.Now;
                        break;
                }
            }
        }

        private void RowVersionAttribute()
        {
            foreach (var entry in ChangeTracker.Entries()
                                               .Where(entry => entry.Entity.GetType()
                                                                           .GetProperties()
                                                                           .Any(p => p.IsDefined(typeof(RowVersionAttribute), true) && p.PropertyType == typeof(int))))
            {
                var propertyName = entry.Entity.GetType()
                                               .GetProperties()
                                               .FirstOrDefault(p => p.IsDefined(typeof(RowVersionAttribute), true) && p.PropertyType == typeof(int))
                                               ?.Name;

                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Property(propertyName).CurrentValue = 1;
                        break;

                    case EntityState.Modified:
                        if (int.TryParse(entry.Property(propertyName).CurrentValue.ToString(), out int currentValue))
                            entry.Property(propertyName).CurrentValue = currentValue + 1;
                        break;
                }
            }
        }

        private int LogEntities(Func<int> saveChangesOriginal)
        {
            var entitiesToLog = ChangeTracker.Entries().Where(MustLogEntity).ToList();
            var entitiesAdded = entitiesToLog.Where(entityEntry => entityEntry.State == EntityState.Added).ToList();
            var entitiesModified = entitiesToLog.Where(entityEntry => entityEntry.State != EntityState.Added).ToList();

            var entitiesModifiedLog = entitiesModified.Select(entityEntry => entityEntry.State == EntityState.Modified ? LogModifiedEntity(entityEntry) : LogDeletedEntity(entityEntry))
                                                      .ToList();

            var rowsAffected = saveChangesOriginal();

            var entitiesAddedLog = entitiesAdded.AsParallel()
                                                .Select(LogAddedEntity)
                                                .ToList();

            Set<LogEntity>().AddRange(entitiesModifiedLog);
            Set<LogEntity>().AddRange(entitiesAddedLog);

            rowsAffected += saveChangesOriginal();
            return rowsAffected;
        }

        private LogEntity LogAddedEntity(EntityEntry entityEntry)
        {
            var logEntity = new LogEntity
            {
                EntityName = entityEntry.Entity.GetType().Name,
                EntityFullName = entityEntry.Entity.GetType().FullName,
                EntityAssembly = entityEntry.Entity.GetType().Assembly.GetName().Name,
                LogActionType = LogActionType.Insert,
                OriginalValues = JsonConvert.SerializeObject(entityEntry.Entity, new JsonSerializerSettings
                {
                    ContractResolver = new SimpleTypeContractResolver()
                }),
                CreatedAt = DateTime.Now,
                TransactionId = Database.CurrentTransaction.TransactionId.ToString()
            };

            return logEntity;
        }

        private LogEntity LogDeletedEntity(EntityEntry entityEntry)
        {
            var logEntity = new LogEntity
            {
                EntityName = entityEntry.Entity.GetType().Name,
                EntityFullName = entityEntry.Entity.GetType().FullName,
                EntityAssembly = entityEntry.Entity.GetType().Assembly.GetName().Name,
                LogActionType = LogActionType.Delete,
                OriginalValues = JsonConvert.SerializeObject(entityEntry.Entity, new JsonSerializerSettings
                {
                    ContractResolver = new SimpleTypeContractResolver()
                }),
                CreatedAt = DateTime.Now,
                TransactionId = Database.CurrentTransaction.TransactionId.ToString()
            };

            return logEntity;
        }

        private LogEntity LogModifiedEntity(EntityEntry entityEntry)
        {
            var databaseValues = entityEntry.GetDatabaseValues();
            var modifiedEntityProperties = entityEntry.OriginalValues
                                                      .Properties
                                                      .Where(p =>
                                                      {
                                                          var originalValue = (databaseValues[p.Name] ?? string.Empty).ToString();
                                                          var currentValue = (entityEntry.CurrentValues[p.Name] ?? string.Empty).ToString();

                                                          return entityEntry.Property(p.Name).IsModified && originalValue != currentValue;
                                                      })
                                                      .Select(p => new ModifiedEntityProperty
                                                      {
                                                          PropertyName = p.Name,
                                                          OldValue = (databaseValues[p.Name] ?? string.Empty).ToString(),
                                                          NewValue = (entityEntry.CurrentValues[p.Name] ?? string.Empty).ToString()
                                                      })
                                                      .ToList();
            var logEntity = new LogEntity
            {
                EntityName = entityEntry.Entity.GetType().Name,
                EntityFullName = entityEntry.Entity.GetType().FullName,
                EntityAssembly = entityEntry.Entity.GetType().Assembly.GetName().Name,
                LogActionType = LogActionType.Update,
                OriginalValues = JsonConvert.SerializeObject(entityEntry.GetDatabaseValues().ToObject(), new JsonSerializerSettings
                {
                    ContractResolver = new SimpleTypeContractResolver()
                }),
                ChangedValues = JsonConvert.SerializeObject(modifiedEntityProperties),
                CreatedAt = DateTime.Now,
                TransactionId = Database.CurrentTransaction.TransactionId.ToString()
            };

            return logEntity;
        }

        private bool MustLogEntity(EntityEntry entityEntry)
        {
            if (!entityEntry.Entity.GetType().IsDefined(typeof(EntityLogAttribute), false))
                return entityStatesToLog.Contains(entityEntry.State);

            var entityLogAttribute = entityEntry.Entity.GetType().GetCustomAttributes(false)
                                                                 .OfType<EntityLogAttribute>()
                                                                 .FirstOrDefault();

            if (entityLogAttribute.IgnoreEntity)
                return false;

            var logActionType = EntityStateToLogActionType(entityEntry.State);

            return logActionType.HasValue && entityLogAttribute.LogActionType.HasFlag(logActionType.Value);
        }

        private LogActionType? EntityStateToLogActionType(EntityState entityState)
        {
            switch (entityState)
            {
                case EntityState.Deleted:
                    return LogActionType.Delete;
                case EntityState.Modified:
                    return LogActionType.Update;
                case EntityState.Added:
                    return LogActionType.Insert;
                default:
                    return null;
            }
        }
    }
}